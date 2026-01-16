export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber?: string;
}

export interface AuthResponse {
  userId: number;
  email: string;
  fullName: string;
  token: string;
  roles: string[];
}
