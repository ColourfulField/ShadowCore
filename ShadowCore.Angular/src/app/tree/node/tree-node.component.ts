import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-tree-node',
  templateUrl: './tree-node.component.html',
  styleUrls: ['./tree-node.component.less']
})
export class TreeNodeComponent {
  @Input() name: string;

  constructor() {

  }
}
