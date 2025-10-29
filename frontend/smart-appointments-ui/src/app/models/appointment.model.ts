export interface Appointment {
  id?: number;
  providerId: number;
  clientId: number;
  doctorName: string;
  date: string;
  time: string;
  status?: string; // e.g., "Pending", "Confirmed", "Cancelled"
  comment?: string;
}
