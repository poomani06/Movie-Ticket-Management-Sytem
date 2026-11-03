export interface Booking {
  bookingId: number;
  bookingReference: string;
  showId: number;
  customerName: string;
  customerEmail: string;
  customerPhone: string;
  bookingDate: string;
  totalAmount: number;
  status: string;
}

export interface CreateBooking {
  showId: number;
  seatIds: number[];
}