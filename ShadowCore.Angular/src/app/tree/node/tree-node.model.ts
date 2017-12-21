export interface ITreeNode {
  id: any;
  name: string;
  data: any;
  parent: ITreeNode;
  children: Array<ITreeNode>;
}

export class TreeNode implements ITreeNode {
  public id: any = null;
  public name: string = '';
  public data: any = null;
  public parent: ITreeNode = null;
  public children: Array<ITreeNode> = null;

  constructor(fields?: {
    id?: any;
    name?: string;
    data?: any;
    parent?: ITreeNode;
    children?: Array<ITreeNode>;
  }) {
    if (fields) {
      Object.assign(this, fields);
    }
  }
}
//
// export class BasicTreeNodeModel extends TreeNodeModel{
//   readonly isHidden: boolean;
//   readonly isExpanded: boolean;
//   readonly isActive: boolean;
//   readonly isFocused: boolean;
//   allowDrop: (draggedElement: any) => boolean;
//   index: number;
//   position: number;
//   height: number;
//   readonly level: number;
//   readonly path: string[];
//   readonly elementRef: any;
//   private _originalNode;
//   readonly originalNode: any;
//   readonly hasChildren: boolean;
//   readonly isCollapsed: boolean;
//   readonly isLeaf: boolean;
//   readonly isRoot: boolean;
//   readonly displayField: any;
//
// }
