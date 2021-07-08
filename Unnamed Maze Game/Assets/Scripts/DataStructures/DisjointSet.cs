using System.Collections.Generic;

public class DisjointSet {
    private Dictionary<object, int> sizeDictionary = new Dictionary<object, int>();
    private Dictionary<object, object> parentDictionary = new Dictionary<object, object>();

    public void changeSize(object node, int value) {
        sizeDictionary[node] = value;
    }
    
    public void changeParent(object child, object parent) {
        parentDictionary[child] = parent;
    }
    
    public void WeightedUnion(object cell1, object cell2) {
        object root1 = Root(cell1);
        object root2 = Root(cell2);
        if (sizeDictionary[root1] < sizeDictionary[root2]) {
            parentDictionary[root1] = parentDictionary[root2];
            sizeDictionary[root2] += sizeDictionary[root1];
        }
        else {
            parentDictionary[root2] = parentDictionary[root1];
            sizeDictionary[root1] += sizeDictionary[root2];
        }
    }

    private object Root(object cell) {
        while (parentDictionary[cell] != cell) {
            parentDictionary[cell] = parentDictionary[parentDictionary[cell]];
            cell = parentDictionary[cell];
        }

        return cell;
    }

    public bool Find(object cell1, object cell2) {
        return Root(cell1) == Root(cell2);
    }
    
}