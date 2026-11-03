export interface Show {
  showId: number;
  movieId: number;
  screenId: number;
  startTime: string;
  endTime: string;
  ticketPrice: number;
}

export interface CreateShow {
  movieId: number;
  screenId: number;
  startTime: string;
  endTime: string;
  ticketPrice: number;
}