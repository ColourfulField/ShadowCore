import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { TreeNodeComponent } from './Tree/node/tree-node.component';
import { TreeModule } from 'angular2-tree-component';
import { TreeComponent } from './tree/tree.component';
import { NodeDescriptionComponent } from './tree/node-description/node-description.component';

@NgModule({
  declarations: [
    AppComponent,
    TreeNodeComponent,
    TreeComponent,
    NodeDescriptionComponent,
  ],
  imports: [
    BrowserModule,
    TreeModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
