using System.Linq;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization
{
    
    // ToDo: some functions could be modified and reused, for simplicity sake letting them be
    public class ItemLineOrganizer : IItemLineOrganizer
    {

        public void Initialize()
        {
            
        }

        public void CleanUp()
        {
            
        }

        public bool TryBuildItemConfiguration(ItemLineComponent originalItemLine, ItemContainerComponent item, ref int index, ref ItemContainerComponent[] itemConfiguration)
        {
            return TryBuildItemConfiguration(originalItemLine.ItemContainerComponents, item, ref index, ref itemConfiguration);
        }

        public bool TryBuildItemConfiguration(ItemContainerComponent[] originalItemLine, ItemContainerComponent item, ref int index, ref ItemContainerComponent[] result)
        {
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

        public bool RemoveItem(ItemLineComponent itemLineComponent, ItemContainerComponent itemComponent)
        {
            return RemoveItem(itemLineComponent.ItemContainerComponents, itemComponent);
        }

        public bool RemoveItem(ItemContainerComponent[] itemLine, ItemContainerComponent itemComponent)
        {
            int lengthLeft = itemComponent.Size;

            int i = 0;
            
            // searching for first hit
            for (; i < itemLine.Length; i++)
            {
                if (itemLine[i] == itemComponent)
                {
                    break;
                }
            }

            // no hits
            if (i == itemLine.Length)
            {
                return false;
            }

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