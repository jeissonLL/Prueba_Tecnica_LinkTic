import { API_URL } from '../../env';
import type { Booking } from '../api/interfaces/booking';

// Crear una nueva reserva
export const createBooking = async (booking: Booking): Promise<void> => {
  const res = await fetch(`${API_URL}/Booking`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(booking),
  });
  if (!res.ok) throw new Error('Failed to create booking');
};

// Actualizar una reserva existente
export const updateBooking = async (booking: Booking): Promise<void> => {
  const res = await fetch(`${API_URL}/Booking`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(booking),
  });
  if (!res.ok) throw new Error('Failed to update booking');
};

// Obtener la lista de reservas
export const getBookings = async (): Promise<Booking[]> => {
  const res = await fetch(`${API_URL}/Booking`);
  if (!res.ok) throw new Error('Failed to fetch bookings');
  return res.json();
};

// Obtener una reserva por ID
export const getBookingById = async (id: number): Promise<Booking> => {
  const response = await fetch(`${API_URL}/Booking/${id}`);
  if (!response.ok) {
    throw new Error(`Error fetching booking with ID ${id}`);
  }
  return await response.json();
};

//Eliminar reserva
export const deleteBooking = async (id: number): Promise<void> => {
    const res = await fetch(`${API_URL}/Booking/${id}`, {
      method: 'DELETE',
    });
    if (!res.ok) {
        const errorMessage = await res.text(); 
        throw new Error(`Failed to delete booking with ID ${id}: ${errorMessage}`);
      }
  };