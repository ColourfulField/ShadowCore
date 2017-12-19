import { Component, OnInit } from '@angular/core';
import {TreeNode} from './node/tree-node.model';


@Component({
  selector: 'app-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.less'],
})
export class TreeComponent implements OnInit {
    nodes: TreeNode[]
  constructor() { }

  ngOnInit() {
    this.nodes = [
      new TreeNode({
        id: 1,
        name: 'root1',
        children: [
          new TreeNode({ id: 2, name: 'child1' }),
          new TreeNode({ id: 3, name: 'child2' })
        ],
      }),
      new TreeNode({
        id: 4,
        name: 'root2',
        children: [
          new TreeNode ({ id: 5, name: 'child2.1' }),
          new TreeNode({
            id: 6,
            name: 'child2.2',
            children: [
              new TreeNode({ id: 7, name: 'subsub' }),
              new TreeNode({
                id: 4,
                name: 'root2',
                children: [
                  new TreeNode ({ id: 5, name: 'child2.1' }),
                  new TreeNode({
                    id: 6,
                    name: 'child2.2',
                    children: [
                      new TreeNode({ id: 7, name: 'subsub' }),
                      new TreeNode({
                        id: 1,
                        name: 'root1',
                        children: [
                          new TreeNode({ id: 2, name: 'child1' }),
                          new TreeNode({ id: 3, name: 'child2' })
                        ],
                      }),
                      new TreeNode({
                        id: 4,
                        name: 'root2',
                        children: [
                          new TreeNode ({ id: 5, name: 'child2.1' }),
                          new TreeNode({
                            id: 6,
                            name: 'child2.2',
                            children: [
                              new TreeNode({ id: 7, name: 'subsub' }),
                              new TreeNode({
                                id: 4,
                                name: 'root2',
                                children: [
                                  new TreeNode ({ id: 5, name: 'child2.1' }),
                                  new TreeNode({
                                    id: 6,
                                    name: 'child2.2',
                                    children: [
                                      new TreeNode({ id: 7, name: 'subsub' }),
                                      new TreeNode({
                                        id: 1,
                                        name: 'root1',
                                        children: [
                                          new TreeNode({ id: 2, name: 'child1' }),
                                          new TreeNode({ id: 3, name: 'child2' })
                                        ],
                                      }),
                                      new TreeNode({
                                        id: 4,
                                        name: 'root2',
                                        children: [
                                          new TreeNode ({ id: 5, name: 'child2.1' }),
                                          new TreeNode({
                                            id: 6,
                                            name: 'child2.2',
                                            children: [
                                              new TreeNode({ id: 7, name: 'subsub' }),
                                              new TreeNode({
                                                id: 4,
                                                name: 'root2',
                                                children: [
                                                  new TreeNode ({ id: 5, name: 'child2.1' }),
                                                  new TreeNode({
                                                    id: 6,
                                                    name: 'child2.2',
                                                    children: [
                                                      new TreeNode({ id: 7, name: 'subsub' }),
                                                      new TreeNode({
                                                        id: 1,
                                                        name: 'root1',
                                                        children: [
                                                          new TreeNode({ id: 2, name: 'child1' }),
                                                          new TreeNode({ id: 3, name: 'child2' })
                                                        ],
                                                      }),
                                                      new TreeNode({
                                                        id: 4,
                                                        name: 'root2',
                                                        children: [
                                                          new TreeNode ({ id: 5, name: 'child2.1' }),
                                                          new TreeNode({
                                                            id: 6,
                                                            name: 'child2.2',
                                                            children: [
                                                              new TreeNode({ id: 7, name: 'subsub' }),
                                                              new TreeNode({
                                                                id: 4,
                                                                name: 'root2',
                                                                children: [
                                                                  new TreeNode ({ id: 5, name: 'child2.1' }),
                                                                  new TreeNode({
                                                                    id: 6,
                                                                    name: 'child2.2',
                                                                    children: [
                                                                      new TreeNode({ id: 7, name: 'subsub' })
                                                                    ]
                                                                  })
                                                                ]
                                                              })
                                                            ]
                                                          })
                                                        ]
                                                      })
                                                    ]
                                                  })
                                                ]
                                              })
                                            ]
                                          })
                                        ]
                                      })
                                    ]
                                  })
                                ]
                              })
                            ]
                          })
                        ]
                      })
                    ]
                  })
                ]
              })
            ]
          })
        ]
      })
    ];
  }



}
