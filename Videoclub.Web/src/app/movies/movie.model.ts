export interface Movie {
  id?: number;
  title: string;
  description: string;
  category: string;
  photoURL: string;
  state: string;
  rentDate?: Date;
  userId?: number;
  usernameRented?: string;
}

export interface RentHistory {
  movieId: number;
  userId: number;
  rentDate: string;
  returnDate: string | null;
}

export const ALL_CATEGORIES = 'All';
export const ALL_STATES = 'All';
export const ALL_TEXT_TO_FIND = '';
