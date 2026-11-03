export interface Screen {
  screenId: number;
  theatreId: number;
  screenName: string;
}

export interface CreateScreen {
  theatreId: number;
  screenName: string;
}