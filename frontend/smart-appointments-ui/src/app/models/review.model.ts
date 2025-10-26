export interface Review {
  id?: number;
  providerId: number;
  clientId: number;
  rating: number;
  comment: string;
  createdAt?: string;
}
