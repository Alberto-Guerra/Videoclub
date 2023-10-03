export interface AuthState {
  isAuthenticated: boolean;
  username?: string;
  userId?: string;
}

export const initialState: AuthState = {
  isAuthenticated: false,
};
