import { API_URL } from '../../env';
import type { Service } from '../api/interfaces/service';

export const getService = async (): Promise<Service[]> => {
  const res = await fetch(`${API_URL}/Services`);
  if (!res.ok) throw new Error('Failed to fetch customers');
  return res.json();
};

export const getServiceById = async (id: number): Promise<Service> => {
  const res = await fetch(`${API_URL}/Services/${id}`);
  if (!res.ok) throw new Error(`Failed to fetch customer with ID ${id}`);
  return res.json();
};

export const createService = async (service: Service): Promise<void> => {
  const res = await fetch(`${API_URL}/Services`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(service),
  });
  if (!res.ok) throw new Error('Failed to create customer');
};

export const updateService = async (service: Service): Promise<void> => {
  const res = await fetch(`${API_URL}/Services`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(service),
  });
  if (!res.ok) throw new Error('Failed to update customer');
};

export const deleteService = async (id: number): Promise<void> => {
  const res = await fetch(`${API_URL}/Services`, {
    method: 'DELETE',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ id }),
  });
  if (!res.ok) throw new Error('Failed to delete customer');
};
