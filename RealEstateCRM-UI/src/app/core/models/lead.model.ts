export interface Lead {
  id: number;
  customerId: number;
  customerName?: string;
  propertyId?: number;
  propertyTitle?: string;
  statusId: number;
  statusName?: string;
  statusColor?: string;
  budget?: number;
  notes?: string;
  nextFollowUpDate?: Date;
  createdByUserId: number;
  createdByUserName?: string;
  createdAt: Date;
  closedAt?: Date;
  updatedAt?: Date;
}

export interface CreateLeadRequest {
  customerId: number;
  propertyId?: number;
  statusId: number;
  budget?: number;
  notes?: string;
  nextFollowUpDate?: Date;
}

export interface UpdateLeadRequest {
  id: number;
  statusId: number;
  budget?: number;
  notes?: string;
  nextFollowUpDate?: Date;

}


