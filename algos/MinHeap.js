/**
 * https://www.cs.usfca.edu/~galles/visualization/Heap.html
 * Class to represent a MinHeap which is a Priority Queue optimized for fast
 * retrieval of the minimum value. It is implemented using an array but it is
 * best visualized as a tree structure where each 'node' has left and right
 * children except the 'node' may only have a left child.
 * - parent is located at: Math.floor(i / 2);
 * - left child is located at: i * 2
 * - right child is located at: i * 2 + 1
 */
class MinHeap {
    constructor() {
        /**
         * 0th index not used, so null is a placeholder. Not using 0th index makes
         * calculating the left and right children's index cleaner.
         * This also effectively makes our array start at index 1.
         */
        this.heap = [null];
    }

    //helper functions for finding parent / children of an index
    idxOfParent(index) {
        return Math.floor(index / 2);
    }

    // currentIdex
    // parentOfCurrent = this.idxOfParent(currentIdx)

    idxOfLeftChild(index) {
        return index * 2;
    }

    idxOfRightChild(index) {
        return index * 2 + 1;
    }

    /**
     * Retrieves the top (minimum number) in the heap without removing it.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @returns {?number} Null if empty.
     */
    top() { 
        return this.heap[1] ? this.heap[1] : null;
    }

    /**
     * Inserts a new number into the heap and maintains the heaps order.
     * 1. Push new num to back of the heap array.
     * 2. Iteratively swap the new num with it's parent while it is smaller than
     *    it's parent.
     * - Time: O(log n) logarithmic due to shiftUp / iterative swapping.
     * - Space: O(1) constant.
     * @param {number} num The num to add.
     */
    insert(num) { 
        //add item to heap
        this.heap.push(num);

        //get index of new item
        let insertedIdx = this.heap.length - 1;
        //get index of new item's parent
        let parentIdx = this.idxOfParent(insertedIdx);
        //while value at insertion point less than its parent, swap and recalculate parent
        while (this.heap[insertedIdx] < this.heap[parentIdx] && parentIdx >= 1) {
            //perform swap
            let temp = this.heap[insertedIdx];
            this.heap[insertedIdx] = this.heap[parentIdx];
            this.heap[parentIdx] = temp;
            //swapped value is now at old parent
            insertedIdx = parentIdx;
            //get parent of old parent
            parentIdx = this.idxOfParent(insertedIdx);
        }
        return this;
    }

    /**
     * Logs the tree horizontally with the root on the left and the index in
     * parenthesis using reverse inorder traversal.
     */
    printHorizontalTree(parentIdx = 1, spaceCnt = 0, spaceIncr = 10) {
        if (parentIdx > this.heap.length - 1) {
            return;
        }

        spaceCnt += spaceIncr;
        this.printHorizontalTree(parentIdx * 2 + 1, spaceCnt);

        console.log(
            " ".repeat(spaceCnt < spaceIncr ? 0 : spaceCnt - spaceIncr) +
            `${this.heap[parentIdx]} (${parentIdx})`
        );

        this.printHorizontalTree(parentIdx * 2, spaceCnt);
    }

    /**
     * Extracts the min num from the heap and then re-orders the heap to
     * maintain order so the next min is ready to be extracted.
     * 1. Save the first node to a temp var.
     * 2. Pop last node off and set idx1 equal to the popped value.
     * 3. Iteratively swap the old last node that is now at idx1 with it's
     *    smallest child IF the smallest child is smaller than it.
     * - Time: O(log n) logarithmic due to shiftDown.
     * - Space: O(1) constant.
     * @returns {?number} The min number or null if empty.
    */
    extract() { 

        if (this.heap.length == 1) return null;
        if (this.heap.length == 2) return this.heap.pop()

        // store min for return
        let min = this.heap[1]
        
        // pop last item off and overwrite the previous minimum
        let lastItem = this.heap.pop()
        this.heap[1] = lastItem
        
        //initializing parent, left and right
        let parent = 1;
        let left = 2;
        let right = 3;
        
        //as along as left is less than the length, we have at least one child to look at it still
        while(left < this.heap.length){
            //determine which child holds a smaller value (make sure right is in bounds)
            let smaller = left;
            if (right < this.heap.length && this.heap[right] < this.heap[left]){
                smaller = right
            }

            //if the smaller value is smaller than the current parent..
            if (this.heap[smaller] < this.heap[parent]){
                //perform swap
                let temp = this.heap[parent]
                this.heap[parent] = this.heap[smaller]
                this.heap[smaller] = temp
                //reset variables for next iteration
                parent = smaller
                left = this.idxOfLeftChild(parent)
                right = this.idxOfRightChild(parent)
            } else {
                break; //if no swap was performed, we're done shifting down
            }
        }
        //return the extracted value
        return min
    }
}


let testHeap = new MinHeap();

testHeap.insert(10);
testHeap.insert(7);
testHeap.insert(3);
testHeap.insert(4);
testHeap.insert(5);
testHeap.printHorizontalTree()


console.log("\n\n********\n\n");
console.log("extracted: ", testHeap.extract())
testHeap.printHorizontalTree();

console.log("\n\n********\n\n");
console.log("extracted: ",testHeap.extract())
testHeap.printHorizontalTree();