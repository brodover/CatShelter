import { Color, Pattern, Stat } from "./const";

export interface Cat {
  Id: string;
  OwnerId: string;
  Name: string;
  Pattern: Pattern;
  Color: Color;
  Stats: Stat;

  _showButton: boolean;
}

export interface Message {
  Username: string;
  Content: string;
}
