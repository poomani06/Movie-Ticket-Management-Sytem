export interface Seat {
  seatId: number;
  screenId: number;
  rowName: string;
  seatNumber: number;
  seatType: string;
}

export interface CreateSeat {
  screenId: number;
  rowName: string;
  seatNumber: number;
  seatType: string;
}