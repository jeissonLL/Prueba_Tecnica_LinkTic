<script lang="ts">
  import { goto } from '$app/navigation';
  import { getBookingById } from '$lib/api/booking';
  import { getCustomers } from '$lib/api/customer'; 
  import { getService } from '$lib/api/service';
  import type { Booking } from '$lib/api/interfaces/booking';
  import type { Customer } from '$lib/api/interfaces/customer';
  import type { Service } from '$lib/api/interfaces/service';
  import { onMount } from 'svelte';

  let booking: Booking | null = null;
  let customer: Customer | null = null;
  let service: Service | null = null;
  let message: string = '';
  let messageType: 'error' | 'success' | '' = '';
  let showMessage = false;

  onMount(async () => {
    const url = window.location.href;
    const urlParts = url.split('/');
    const bookingId = parseInt(urlParts[urlParts.length - 1]);

    if (!isNaN(bookingId)) {
      try {
        booking = await getBookingById(bookingId); 
        message = 'Detalles de la reserva cargados correctamente.';
        messageType = 'success';
        showMessage = true;

        if (booking) {
          customer = await getCustomers().then(customers => customers.find(c => c.customerId === booking?.customerId) || null);
          service = await getService().then(services => services.find(s => s.serviceId === booking?.serviceId) || null);
        }

        setTimeout(() => {
          showMessage = false;
        }, 3000);
      } catch (error) {
        console.error('Error al obtener la reserva:', error);
        message = 'Error al obtener los detalles de la reserva.';
        messageType = 'error';
        showMessage = true;

        setTimeout(() => {
          showMessage = false;
        }, 3000); 
      }
    } else {
      message = 'ID de reserva inválido';
      messageType = 'error';
      showMessage = true;

      setTimeout(() => {
        showMessage = false;
      }, 3000);
    }
  });
</script>

<style>
  .toast {
    padding: 15px;
    margin-bottom: 20px;
    border-radius: 8px;
    font-size: 16px;
    font-weight: bold;
    display: flex;
    align-items: center;
    max-width: 500px;
    margin: 0 auto;
    opacity: 0;
    transform: translateY(-20px);
    transition: opacity 0.5s ease, transform 0.5s ease;
  }

  /* Estilo para mensaje de error */
  .toast.error {
    background-color: #f8d7da;
    color: #721c24;
    border: 1px solid #f5c6cb;
  }

  /* Estilo para mensaje de éxito */
  .toast.success {
    background-color: #d4edda;
    color: #155724;
    border: 1px solid #c3e6cb;
  }

  .toast.show {
    opacity: 1;
    transform: translateY(0);
  }

  /* Estilos para los botones */
  .button {
    padding: 10px 20px;
    margin-top: 20px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 16px;
    transition: background-color 0.3s;
    text-align: center;
    display: inline-block;
  }

  .button:hover {
    background-color: #0056b3;
  }

  .button:focus {
    outline: none;
  }

  /* Estilos generales de la página */
  h1 {
    text-align: center;
    margin-top: 20px;
  }

  /* Estilo del contenedor de detalles de la reserva */
  .booking-details {
    max-width: 600px;
    margin: 20px auto;
  }

  /* Alineación central para el mensaje de éxito/error */
  .alert-container {
    display: flex;
    justify-content: center;
  }

  /* Estilos para la tabla */
  table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
  }

  table th, table td {
    padding: 12px;
    text-align: left;
    border: 1px solid #ddd;
  }

  table th {
    background-color: #f2f2f2;
    font-weight: bold;
  }

  table td {
    background-color: #f9f9f9;
  }
</style>

<h1>Detalles de la Reserva</h1>

{#if showMessage}
  <div class="alert-container">
    <div class="toast {messageType} {showMessage ? 'show' : ''}">
      {message}
    </div>
  </div>
{/if}

{#if booking}
  <div class="booking-details">
    <table>
      <thead>
        <tr>
          <th>Campo</th>
          <th>Valor</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td><strong>Fecha de Reserva</strong></td>
          <td>{booking.reservationDate}</td>
        </tr>
        <tr>
          <td><strong>Número de Personas</strong></td>
          <td>{booking.peopleNumber}</td>
        </tr>
        <tr>
          <td><strong>Estado</strong></td>
          <td>{booking.estado}</td>
        </tr>
        <tr>
          <td><strong>Nombre del Cliente</strong></td>
          <td>{customer?.name || 'Sin asignar'}</td>
        </tr>
        <tr>
          <td><strong>Nombre del Servicio</strong></td>
          <td>{service?.serviceName || 'Sin asignar'}</td>
        </tr>
      </tbody>
    </table>
  </div>
{/if}

<div class="alert-container">
  <button class="button" on:click={() => goto('/bookings')}>Regresar</button>
</div>
