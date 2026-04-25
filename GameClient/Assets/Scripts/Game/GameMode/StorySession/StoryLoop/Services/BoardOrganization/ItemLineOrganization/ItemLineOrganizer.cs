using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    
    // ToDo: some functions could be modified and reused, for simplicity sake letting them be
    // ToDo: now with some idea it could be overwritten
    public class ItemLineOrganizer : IItemLineOrganizer
    {
        
        public bool TryBuildItemConfiguration(ItemLineComponent originalItemLine, ItemContainerComponent item, ref int index, ItemContainerComponent[] result)
        {
            return TryBuildItemConfiguration(originalItemLine.ItemContainerComponents, item, ref index, result);
        }

        public bool TryBuildItemConfiguration(ItemContainerComponent[] originalItemLine, ItemContainerComponent item, ref int index, ItemContainerComponent[] result)
        {
            int emptySpaces = originalItemLine.Count(x => x is null);
            if (emptySpaces < item.Size)
            {
                return false;
            }
            
            ItemContainerComponent[] originalConfiguration = originalItemLine;
            
            // copying original configuration
            for (int i = 0; i < originalConfiguration.Length; i++)
            {
                result[i] = originalConfiguration[i];
            }
            
            // Checking for out-of-bounds
            index = Mathf.Min(index, result.Length - item.Size);
            
            // trying to insert item without changes
            if (TryInsertWithoutMovement(item, result, index))
            {
                return true;
            }

            // compressing to right
            if (TryGetMovementToRight(result, index, item.Size, out int startMovementIndex, out int endMovementIndex))
            {
                CompressRight(result, startMovementIndex, endMovementIndex);

                if (TryInsertWithoutMovement(item, result, index))
                {
                    return true;
                }
            }
            
            // compressing to left
            int searchStartIndex = index + item.Size - 1;
            if (TryGetMovementToLeft(result, searchStartIndex, item.Size, out startMovementIndex, out endMovementIndex))
            {
                CompressLeft(result, startMovementIndex, endMovementIndex);
                
                if (TryInsertWithoutMovement(item, result, index))
                {
                    return true;
                }
            }
            
            return false;
        }
        
        // ToDo: I think there is still bug left, but I am not sure, as there was few changes after I encountered it once, so marking as assume it compromised
        public bool TryMakeSwap(
            Vector3 position, 
            int targetItemOriginalIndex, 
            ItemLineComponent originalLine, 
            ItemLineComponent targetLine, 
            ItemContainerComponent targetItem, 
            ItemContainerComponent[] originalLineResult, 
            ItemContainerComponent[] targetLineResult)
        {
            if (!TryGetLineIndexForPosition(targetLine, position, out int index))
            {
                return false;
            }

            // calculating spaces required for swap on original line
            int spacesRequired = 0;
            bool overflowLeft = false;
            // no need to think about duplicates
            HashSet<ItemContainerComponent> itemsToMoveToOriginal = new (3);
            
            // to the left
            if (index > 0 && 
                targetLine.ItemContainerComponents[index] is not null)
            {
                itemsToMoveToOriginal.Add(targetLine.ItemContainerComponents[index]);
                for (int i = index - 1; i >= 0; i--)
                {
                    if (targetLine.ItemContainerComponents[i] != targetLine.ItemContainerComponents[index]) 
                        break;

                    overflowLeft = true;
                    spacesRequired++;
                }
            }
            
            
            // to the right
            if (index < targetLine.ItemContainerComponents.Length - 1 && 
                targetLine.ItemContainerComponents[index + targetItem.Size - 1] is not null)
            {
                itemsToMoveToOriginal.Add(targetLine.ItemContainerComponents[index + targetItem.Size - 1]);
                for (int i = index + targetItem.Size - 1; i + 1 < targetLine.ItemContainerComponents.Length; i++)
                {
                    if (targetLine.ItemContainerComponents[i] != targetLine.ItemContainerComponents[i + 1]) 
                        break;

                    spacesRequired++;
                }
            }
            
            
            // under the item
            for (int i = 0; i < targetItem.Size; i++)
            {
                if (targetLine.ItemContainerComponents[index + i] is null) 
                    continue;
                   
                itemsToMoveToOriginal.Add(targetLine.ItemContainerComponents[index + i]);
                spacesRequired++;
            }

            // for case when we can make partial move between lines with strict condition
            // no overflow left
            // swapping all fully overlapped, to target item original side, and then moving items to the right, so we can clean some space
            if (!overflowLeft)
            {
                List<ItemContainerComponent> fullyOverlappedItems = new List<ItemContainerComponent>();
                int freedablePositions = 0;
                
                // searching for fully overlapped items
                foreach (ItemContainerComponent itemToMoveToOriginal in itemsToMoveToOriginal)
                {
                    TryGetLineIndexForPosition(targetLine, itemToMoveToOriginal.GetItemLinePivot(), out int startingIndex);

                    if (startingIndex + itemToMoveToOriginal.Size - 1 < index + targetItem.Size)
                    {
                        fullyOverlappedItems.Add(itemToMoveToOriginal);
                        freedablePositions = itemToMoveToOriginal.Size;
                    }
                }

                int freePositions = targetLine.ItemContainerComponents.Skip(index).Count(x => x is null);

                if (freedablePositions + freePositions >= targetItem.Size)
                {
                    
                    for (int i = 0; i < originalLine.ItemContainerComponents.Length; i++)
                    {
                        originalLineResult[i] = originalLine.ItemContainerComponents[i];
                        targetLineResult[i] = targetLine.ItemContainerComponents[i];
                    }
                    
                    foreach (ItemContainerComponent moveToOriginal in fullyOverlappedItems)
                    {
                        RemoveItem(targetLineResult, moveToOriginal, out _);
                    }
            
                    foreach (ItemContainerComponent moveToOriginal in fullyOverlappedItems)
                    {
                        for (int i = 0; i < moveToOriginal.Size; i++)
                        {
                            originalLineResult[targetItemOriginalIndex + i] = moveToOriginal;
                        }

                        targetItemOriginalIndex += moveToOriginal.Size;
                    }

                    TryGetMovementToRight(targetLineResult, index, targetItem.Size, out int start, out int end);
                    CompressRight(targetLineResult, start, end);
                    TryInsertWithoutMovement(targetItem, targetLineResult, index);
                    
                    return true;
                }
            }
            
            
            // PERFECT SWAP HANDLING
            if (spacesRequired <= targetItem.Size)
            {
                Vector3 itemPivotPosition = itemsToMoveToOriginal.Select(x => x.GetItemLinePivot()).OrderBy(x => x.x).First();
                TryGetLineIndexForPosition(targetLine, itemPivotPosition, out int targetLineIndex);
                
                while (targetLineIndex - 1 >= 0 && targetLine.ItemContainerComponents[targetLineIndex - 1] is null)
                {
                    targetLineIndex--;
                }
                
                HandlePerfectSwap(targetItemOriginalIndex, index/*targetLineIndex*/, originalLine, targetLine, targetItem, originalLineResult, targetLineResult, itemsToMoveToOriginal);
                return true;
            }


            while (targetItemOriginalIndex - 1 >= 0 && originalLine.ItemContainerComponents[targetItemOriginalIndex - 1] is null)
            {
                targetItemOriginalIndex--;
            }
            

            // calculating from fist available for movement position
            int originalLineFreeSpaces = originalLine.ItemContainerComponents.Skip(targetItemOriginalIndex).Count(x => x is null);
            
            if (originalLineFreeSpaces < spacesRequired)
            {
                return false;
            }
            
            // attempting to move first covered item to original line, then trying to fit target_item:
            // if it fit -> escape
            // if failed -> move next item

            for (int i = 0; i < originalLine.ItemContainerComponents.Length; i++)
            {
                originalLineResult[i] = originalLine.ItemContainerComponents[i];
                targetLineResult[i] = targetLine.ItemContainerComponents[i];
            }

            List<ItemContainerComponent> targetLineOrderedByPosition = itemsToMoveToOriginal.OrderBy(x => x.GetItemLinePivot().x).ToList();

            // getting left most available point, if no item under target item pivot, using original position 
            if (overflowLeft)
            {
                TryGetLineIndexForPosition(targetLine, targetLineOrderedByPosition[0].GetItemLinePivot(), out index);
            }
            
            for (int i = 0; i < originalLineResult.Length; i++)
            {
                originalLineResult[i] = originalLine.ItemContainerComponents[i];
                targetLineResult[i] = targetLine.ItemContainerComponents[i];
            }
            
            int freeSpacesRequiredToFree = originalLine.ItemContainerComponents.Skip(index).Count(x => x is null);

            int startMovementRight = 0;
            int endMovementRight = 0;
            
            // ToDo get list of items to move instead of determining dynamically
            foreach (ItemContainerComponent fromTargetToOriginalLine in targetLineOrderedByPosition)
            {
                if (!TryInsertWithoutMovement(fromTargetToOriginalLine, originalLineResult, targetItemOriginalIndex))
                {
                    TryGetMovementToRight(originalLineResult, targetItemOriginalIndex, fromTargetToOriginalLine.Size, out startMovementRight, out endMovementRight);
                    CompressRight(originalLineResult, startMovementRight, endMovementRight);
                    TryInsertWithoutMovement(fromTargetToOriginalLine, originalLineResult, targetItemOriginalIndex);
                }

                RemoveItem(targetLineResult, fromTargetToOriginalLine, out _);

                targetItemOriginalIndex += fromTargetToOriginalLine.Size;

                freeSpacesRequiredToFree -= fromTargetToOriginalLine.Size;
                if (freeSpacesRequiredToFree <= 0)
                {
                    break;
                }
            }

            if (!TryInsertWithoutMovement(targetItem, targetLineResult, index))
            {
                TryGetMovementToRight(targetLineResult, index, targetItem.Size, out startMovementRight, out endMovementRight);
                CompressRight(targetLineResult, startMovementRight, endMovementRight);
                TryInsertWithoutMovement(targetItem, targetLineResult, index);
            }
            
            return true;
        }
        
        ///////
        
        /// <summary>
        /// Moved item fully overlaps items in final position
        /// </summary>
        private void HandlePerfectSwap(
            int originalLineIndex, 
            int targetLineIndex, 
            ItemLineComponent originalLine, 
            ItemLineComponent targetLine, 
            ItemContainerComponent item, 
            ItemContainerComponent[] originalLineResult, 
            ItemContainerComponent[] targetLineResult,
            HashSet<ItemContainerComponent> itemsToMoveToOriginal)
        {
            for (int i = 0; i < originalLine.ItemContainerComponents.Length; i++)
            {
                originalLineResult[i] = originalLine.ItemContainerComponents[i];
                targetLineResult[i] = targetLine.ItemContainerComponents[i];
            }

            foreach (ItemContainerComponent moveToOriginal in itemsToMoveToOriginal)
            {
                RemoveItem(targetLineResult, moveToOriginal, out _);
            }
            
            foreach (ItemContainerComponent moveToOriginal in itemsToMoveToOriginal)
            {
                for (int i = 0; i < moveToOriginal.Size; i++)
                {
                    originalLineResult[originalLineIndex + i] = moveToOriginal;
                }

                originalLineIndex += moveToOriginal.Size;
            }

            for (int i = 0; i < item.Size; i++)
            {
                targetLineResult[targetLineIndex + i] = item;
            }
            
        }
        
        ///////
        
        
        public void Organize(Bounds itemLineBounds, ItemContainerComponent[] itemConfiguration)
        {
            ReorganizeInternal(itemLineBounds, itemConfiguration);
        }
        
        public void Organize(ItemLineComponent itemLine, ItemContainerComponent[] itemConfiguration, bool writeIntoItemLine)
        {
            ReorganizeInternal(itemLine.SpriteRenderer.bounds, itemConfiguration);

            if (!writeIntoItemLine)
            {
                return;
            }
            
            for (int i = 0; i < itemLine.ItemContainerComponents.Length; i++)
            {
                itemLine.ItemContainerComponents[i] = itemConfiguration[i];
            }
            
        }

        public bool TryGetLineIndexForPosition(ItemLineComponent itemLineComponent, Vector3 position, out int index)
        {
            if (!IsLocatedInItemLine(itemLineComponent, position))
            {
                index = 0;
                return false;
            }

            float firstItemX = itemLineComponent.FirstItemX;
            float stepX = itemLineComponent.StepX;

            float displacementX = Mathf.Abs(position.x - firstItemX);
            index = Mathf.RoundToInt(displacementX / stepX);

            return true;
        }
        
        public bool IsLocatedInItemLine(ItemLineComponent itemLineComponent, ItemContainerComponent itemComponent)
        {
            return IsLocatedInItemLine(itemLineComponent, itemComponent.transform.position);
        }
        
        public bool IsLocatedInItemLine(ItemLineComponent itemLineComponent, Vector3 position)
        {
            if (!itemLineComponent.isActiveAndEnabled)
            {
                return false;
            }

            Bounds bounds = itemLineComponent.SpriteRenderer.bounds;

            return bounds.Contains(new Vector3(position.x, position.y, bounds.center.z));
        }

        public bool RemoveItem(ItemLineComponent itemLineComponent, ItemContainerComponent itemComponent,
            out int targetItemOriginalIndex)
        {
            return RemoveItem(itemLineComponent.ItemContainerComponents, itemComponent, out targetItemOriginalIndex);
        }

        public bool RemoveItem(ItemContainerComponent[] itemLine, ItemContainerComponent itemComponent, out int targetItemOriginalIndex)
        {
            int lengthLeft = itemComponent.Size;

            int i = 0;
            
            // searching for first hit
            for (; i < itemLine.Length; i++)
            {
                if (itemLine[i] != itemComponent) 
                    continue;
                
                break;
            }

            // no hits
            if (i == itemLine.Length)
            {
                targetItemOriginalIndex = -1;
                return false;
            }

            targetItemOriginalIndex = i;
            
            // cleaning
            for (; lengthLeft > 0; i++)
            {
                itemLine[i] = null;
                lengthLeft--;
            }

            return true;
        }


        // iternal methods
        
        private void ReorganizeInternal(Bounds itemLineViewBounds, ItemContainerComponent[] itemConfiguration)
        {
            ItemContainerComponent itemContainerComponent = itemConfiguration.FirstOrDefault(item => item is not null);

            if (itemContainerComponent is null)
            {
                return;
            }


            Bounds bounds = itemLineViewBounds;

            Vector2 origin = new Vector2(bounds.min.x, bounds.center.y);
            Vector2 step = new Vector2(itemContainerComponent.ItemRenderer.bounds.size.x / itemContainerComponent.Size, 0);
            
            // moving origin to half step to right, so it will be centered on item pivot 
            origin += step / 2;

            itemContainerComponent = null;
            
            // item configuration can be 11|222|3|4|55|6|77, so we need avoid misplacement
            for (int i = 0; i < itemConfiguration.Length; i++)
            {
                // preventing consecutive hits on same item
                if (itemContainerComponent == itemConfiguration[i])
                {
                    continue;
                }

                itemContainerComponent = itemConfiguration[i];
                
                if (itemConfiguration[i] is null)
                {
                    continue;
                }

                float displacementSteps = i + (itemConfiguration[i].Size - 1) / 2f;
                itemConfiguration[i].transform.position = origin + step * displacementSteps;
            }
            
        }

        private bool IsItemPlaceableWithoutMovement(int itemSize, ItemContainerComponent[] currentConfiguration, int index, out int overlapCount)
        {
            overlapCount = 0;

            // no check for out of range needed because position verified preemptively
            for (int i = 0; i < itemSize; i++)
            {
                if (currentConfiguration[index + i] is null)
                    continue; 
                
                overlapCount++;
            }

            return overlapCount == 0;
        }


        private bool TryInsertWithoutMovement(ItemContainerComponent item, ItemContainerComponent[] result, int index)
        {
            bool isItemPlaceableWithoutMovement = IsItemPlaceableWithoutMovement(item.Size, result, index, out int _);

            if (isItemPlaceableWithoutMovement)
            {
                InsertItem(item, index, result);
                return true;
            }

            return false;
        }


        private bool TryGetMovementToRight(ItemContainerComponent[] currentConfiguration, int searchStartIndex, int itemSize, out int startMovementIndex, out int endMovementIndex)
        {
            endMovementIndex = searchStartIndex;
            searchStartIndex = OffsetForItemLeft(currentConfiguration, searchStartIndex);

            // taking into account that we need overshoot, so item stays not separated
            int emptySlotsRequired = itemSize + endMovementIndex - searchStartIndex;
            
            endMovementIndex = searchStartIndex;

            int emptySlotFound = 0;
            int lastEmptyFound = -1;
            int firstItemHit = int.MaxValue;
            
            
            for (int i = endMovementIndex; i < currentConfiguration.Length; i++)
            {
                if (currentConfiguration[i] is not null)
                {
                    firstItemHit = Mathf.Min(firstItemHit, i);
                    continue;
                }
                

                emptySlotFound++;
                lastEmptyFound = i;

                if (emptySlotFound != emptySlotsRequired)
                    continue;

                startMovementIndex = lastEmptyFound;
                endMovementIndex = firstItemHit;
                return true;
            }

            endMovementIndex = firstItemHit;
            startMovementIndex = lastEmptyFound;
            
            return startMovementIndex > endMovementIndex;
        }

        private int OffsetForItemLeft(ItemContainerComponent[] currentConfiguration, int searchStartIndex)
        {
            if (currentConfiguration[searchStartIndex] is null)
            {
                return searchStartIndex;
            }

            for (int i = searchStartIndex - 1; i >= 0; i--)
            {
                if (currentConfiguration[i] != currentConfiguration[searchStartIndex])
                {
                    return i + 1;
                }
            }

            return 0;
        }

        private void CompressRight(ItemContainerComponent[] currentConfiguration, int startMovementIndex, int endMovementIndex)
        {
            int moveIntoIndex = startMovementIndex;
            
            for (int i = startMovementIndex; i >= endMovementIndex; i--)
            {
                if (currentConfiguration[i] is null) 
                    continue;

                currentConfiguration[moveIntoIndex] = currentConfiguration[i];
                currentConfiguration[i] = null;

                moveIntoIndex--;
            }
            
        }

        private bool TryGetMovementToLeft(ItemContainerComponent[] currentConfiguration, int searchStartIndex, int itemSize, out int startMovementIndex, out int endMovementIndex)
        {
            endMovementIndex = searchStartIndex;
            searchStartIndex = OffsetForItemRight(currentConfiguration, searchStartIndex);
            
            // taking into account that we need overshoot, so item stays not separated
            int emptySlotsRequired = itemSize + searchStartIndex - endMovementIndex;
            
            endMovementIndex = searchStartIndex;

            int emptySlotFound = 0;
            int lastEmptyFound = -1;

            // end and start based on first hits
            
            for (int i = endMovementIndex; i > 0; i--)
            {
                if (currentConfiguration[i] is not null)
                    continue;

                emptySlotFound++;
                lastEmptyFound = i;

                if (emptySlotFound != emptySlotsRequired)
                    continue;

                startMovementIndex = i;
                return true;
            }

            startMovementIndex = lastEmptyFound;
            
            return false;
        }

        private int OffsetForItemRight(ItemContainerComponent[] currentConfiguration, int searchStartIndex)
        {
            if (currentConfiguration[searchStartIndex] is null)
            {
                return searchStartIndex;
            }

            for (int i = searchStartIndex + 1; i < currentConfiguration.Length; i++)
            {
                if (currentConfiguration[i] != currentConfiguration[searchStartIndex])
                {
                    return i - 1;
                }
            }

            return currentConfiguration.Length - 1;
        }
        
        private void CompressLeft(ItemContainerComponent[] currentConfiguration, int startMovementIndex, int endMovementIndex)
        {
            int moveIntoIndex = startMovementIndex;
            
            for (int i = startMovementIndex; i <= endMovementIndex; i++)
            {
                if (currentConfiguration[i] is null) 
                    continue;

                currentConfiguration[moveIntoIndex] = currentConfiguration[i];
                currentConfiguration[i] = null;

                moveIntoIndex++;
            }
            
        }
        
        

        private void InsertItem(ItemContainerComponent item, int index, ItemContainerComponent[] itemConfiguration)
        {
            for (int i = 0; i < item.Size; i++)
            {
                itemConfiguration[index + i] = item;
            }
        }
        
        
    }
}