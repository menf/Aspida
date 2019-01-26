import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";

import { DashboardComponent } from "./dashboard.component";
import { NbCardModule, NbInputModule, NbButtonModule } from "@nebular/theme";
import { FormsModule } from "@angular/forms";

@NgModule({
  imports: [
    NbCardModule,
    CommonModule,
    FormsModule,
    NbInputModule,
    NbButtonModule
  ],
  declarations: [DashboardComponent]
})
export class DashboardModule {}
