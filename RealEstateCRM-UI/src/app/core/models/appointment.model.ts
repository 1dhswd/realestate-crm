export interface Appointment {
  id?: number;
  title?: string;
  start: string;
  end: string;
  location?: string;
  notes?: string;
  status: number;
  agentId: number;
  propertyId: number;
  leadId: number;
}
