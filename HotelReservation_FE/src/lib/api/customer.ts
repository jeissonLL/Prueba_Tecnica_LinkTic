import { API_URL } from '../../env';
import type { Customer } from '../api/interfaces/customer';

export const getCustomers = async (): Promise<Customer[]> => {
  const res = await fetch(`${API_URL}/Customer`);
  if (!res.ok) throw new Error('Failed to fetch customers');
  return res.json();
};

export const getCustomerById = async (id: number): Promise<Customer> => {
  const res = await fetch(`${API_URL}/Customer/${id}`);
  if (!res.ok) throw new Error(`Failed to fetch customer with ID ${id}`);
  return res.json();
};

export const createCustomer = async (customer: Customer): Promise<void> => {
  const res = await fetch(`${API_URL}/Customer`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(customer),
  });
  if (!res.ok) throw new Error('Failed to create customer');
};

export const updateCustomer = async (customer: Customer): Promise<void> => {
  const res = await fetch(`${API_URL}/Customer`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(customer),
  });
  if (!res.ok) throw new Error('Failed to update customer');
};

export const deleteCustomer = async (id: number): Promise<void> => {
  const res = await fetch(`${API_URL}/Customer`, {
    method: 'DELETE',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ id }),
  });
  if (!res.ok) throw new Error('Failed to delete customer');
};
