import { Color, Pattern, Stat } from "./const";

export interface Cat {
  Id: string;
  AdopterId: string;
  Name: string;
  Pattern: Pattern;
  Color: Color;
  Stats: Stat;

  _showButton: boolean;
}
