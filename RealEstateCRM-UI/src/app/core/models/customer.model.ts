export interface Customer {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber?: string;
  address?: string;
  statusId: number;
  statusName?: string;
  assignedAgentId?: number;
  assignedAgentName?: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCustomerRequest {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber?: string;
  address?: string;
  statusId: number;
  assignedAgentId?: number;
}

export interface UpdateCustomerRequest extends CreateCustomerRequest {
  id: number;
}
