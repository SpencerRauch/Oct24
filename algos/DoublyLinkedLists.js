/**
 * Class to represent a Node for a DoublyLinkedList.
 */
class DLLNode {
    /**
     * Executed when the new keyword is used to construct a new node instance.
     * @param {any} data The data the new node will store.
     */
    constructor(data) {
        this.data = data;
        /**
         * By default a new node instance will not be connected to any other nodes,
         * these properties will be set after instantiation to connect the node to
         * a list. However, the head prev should remain null.
         *
         * @type {DLLNode|null}
         */
        this.prev = null;
        /** @type {DLLNode|null} */
        this.next = null;
    }
}

/**
 * A class to represent a doubly linked list and contain all of it's methods.
 * A doubly linked list is a singly linked list that can be traversed in both
 * directions.
 */
class DoublyLinkedList {
    /**
     * Executed when the new keyword is used to construct a new DoublyLInkedList
     * instance that inherits these methods and properties.
     */
    constructor() {
        // The list is empty to start.
        /** @type {DLLNode|null} */
        this.head = null;
        /** @type {DLLNode|null} */
        this.tail = null;
    }

    /**
     * Creates a new node and adds it at the front of this list.
     * - Time: O(?).
     * - Space: O(?).
     * @param {any} data The data for the new node.
     * @returns {DoublyLinkedList} This list.
     */
    insertAtFront(data) {
        // Create a New Node Var
        // New Node = data argument
        // next & prev are null
        let newNode = new DLLNode(data);

        // If List is empty
        // New Node is Head & Tail of List
        if (this.head == null) {
            this.head = newNode;
            this.tail = newNode;
            return this;
        }

        // if list is populated
        // look at the Head of the list
        // set this.head.prev to New Node
        this.head.prev = newNode;

        // set next of New Node to Old Head Data
        newNode.next = this.head;

        // set New Node to Head
        this.head = newNode;

        return this;
    }

    /**
     * Creates a new node and adds it at the back of this list.
     * - Time: O(?).
     * - Space: O(?).
     * @param {any} data The data for the new node.
     * @returns {DoublyLinkedList} This list.
     */
    insertAtBack(data) {
        let newTail = new DLLNode(data);
        if (this.isEmpty()) {
            this.head = newTail;
            this.tail = newTail;
        } else {
            this.tail.next = newTail;
            newTail.prev = this.tail;
            this.tail = newTail;
        }
        return this;
    }

    // * EXTRA
    /**
     * Removes the middle node in this list.
     * - Time: O(?).
     * - Space: O(?).
     * @returns {any} The data of the removed node or null if no true middle
     */
    removeMiddleNode() {
        //nothing to remove
        if (this.isEmpty()) return null;

        // when there is only 1 node, it is the middle, remove it.
        if (this.head === this.tail) {
            const removedData = this.head.data;
            this.head = null;
            this.tail = null;
            return removedData;
        }

        let forwardRunner = this.head;
        let backwardsRunner = this.tail;

        while (forwardRunner && backwardsRunner) {
            if (forwardRunner === backwardsRunner) {
                const midNode = forwardRunner;
                midNode.prev.next = midNode.next;
                midNode.next.prev = midNode.prev;
                return midNode.data;
            }
            // runners passed each other without stopping on the same node, even length, we can exit early
            if (forwardRunner.prev === backwardsRunner) {
                return null;
            }
            forwardRunner = forwardRunner.next;
            backwardsRunner = backwardsRunner.prev;
        }
        return null;
    }

    /**
     * Determines if this list is empty.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @returns {boolean} Indicates if this list is empty.
     */
    isEmpty() {
        return this.head === null;
    }

    /**
     * Converts this list to an array of the node's data.
     * - Time: O(n) linear, n = list length.
     * - Space: O(n) linear, array grows as list length increases.
     * @returns {Array<any>} All the data of the nodes.
     */
    toArray() {
        const vals = [];
        let runner = this.head;

        while (runner) {
            vals.push(runner.data);
            runner = runner.next;
        }
        return vals;
    }

    /**
     * Adds all the given items to the back of this list.
     * @param {Array<any>} items Items to be added to the back of this list.
     * @returns {DoublyLinkedList} This list.
     */
    insertAtBackMany(items = []) {
        items.forEach((item) => this.insertAtBack(item));
        return this;
    }
}