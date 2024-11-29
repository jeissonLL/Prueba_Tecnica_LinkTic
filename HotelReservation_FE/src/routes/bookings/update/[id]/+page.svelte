<script lang="ts">
  import { updateBooking, getBookingById } from '$lib/api/booking';
  import { getCustomers } from '$lib/api/customer';
  import { getService } from '$lib/api/service';
  import { goto } from '$app/navigation';
  import type { Booking } from '$lib/api/interfaces/booking';
  import type { Customer } from '$lib/api/interfaces/customer';
  import type { Service } from '$lib/api/interfaces/service';
  import { onMount } from 'svelte';

  let booking: Booking | null = null;
  let customers: Customer[] = [];
  let services: Service[] = [];
  let message: string = '';
  let messageType: 'error' | 'success' | '' = '';
  let showMessage = false;

  const today = new Date().toISOString().split('T')[0]; // Fecha actual en formato YYYY-MM-DD

  const loadBookingAndData = async () => {
    try {
      const url = window.location.href;
      const urlParts = url.split('/');
      const bookingId = parseInt(urlParts[urlParts.length - 1]);

      if (isNaN(bookingId)) {
        throw new Error('ID de reserva inválido');
      }

      booking = await getBookingById(bookingId);
      if (booking && booking.reservationDate) {
        booking.reservationDate = booking.reservationDate.split('T')[0]; // Formato YYYY-MM-DD
      }

      customers = await getCustomers();
      services = await getService();
    } catch (error) {
      console.error('Error al cargar la reserva o los datos:', error);
      message = 'Error al cargar los datos. Inténtelo nuevamente.';
      messageType = 'error';
      showMessage = true;

      setTimeout(() => (showMessage = false), 3000);
    }
  };

  const update = async () => {
    if (booking) {
      try {
        await updateBooking(booking);
        message = 'Reserva actualizada correctamente.';
        messageType = 'success';
        showMessage = true;

        setTimeout(() => {
          showMessage = false;
          goto('/bookings');
        }, 2000);
      } catch (error) {
        console.error('Error al actualizar la reserva:', error);
        message = 'Error al actualizar la reserva.';
        messageType = 'error';
        showMessage = true;

        setTimeout(() => (showMessage = false), 3000);
      }
    }
  };

  onMount(loadBookingAndData);
</script>

<style>
  .form-container {
    max-width: 400px;
    margin: 40px auto;
    padding: 20px;
    background-color: #f9f9f9;
    border-radius: 8px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  }

  h1 {
    text-align: center;
    margin-bottom: 20px;
    font-size: 20px;
    color: #333;
  }

  form {
    display: flex;
    flex-direction: column;
    gap: 15px;
  }

  label {
    font-size: 14px;
    font-weight: bold;
    color: #555;
  }

  input, select {
    width: 100%;
    padding: 8px;
    font-size: 14px;
    border: 1px solid #ddd;
    border-radius: 4px;
  }

  button {
    padding: 10px;
    font-size: 14px;
    font-weight: bold;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    margin-top: 10px;
  }

  button:hover {
    background-color: #0056b3;
  }

  .button-group {
    display: flex;
    justify-content: space-between;
  }

  .toast {
    padding: 10px;
    text-align: center;
    border-radius: 4px;
    font-size: 14px;
    max-width: 400px;
    margin: 10px auto;
  }

  .toast.error {
    background-color: #f8d7da;
    color: #721c24;
    border: 1px solid #f5c6cb;
  }

  .toast.success {
    background-color: #d4edda;
    color: #155724;
    border: 1px solid #c3e6cb;
  }
</style>

<div class="form-container">
  <h1>Actualizar Reserva</h1>

  {#if showMessage}
    <div class="toast {messageType}">
      {message}
    </div>
  {/if}

  {#if booking}
    <form on:submit|preventDefault={update}>
      <div>
        <label for="reservationDate">Fecha de Reserva:</label>
        <input
          id="reservationDate"
          type="date"
          bind:value={booking.reservationDate}
          required
          min={today} />
      </div>
      <div>
        <label for="peopleNumber">Número de Personas:</label>
        <input
          id="peopleNumber"
          type="number"
          bind:value={booking.peopleNumber}
          required min="1" />
      </div>
      <div>
        <label for="estado">Estado:</label>
        <input
          id="estado"
          type="text"
          bind:value={booking.estado}
          required />
      </div>
      <div>
        <label for="customerId">Cliente:</label>
        <select
          id="customerId"
          bind:value={booking.customerId}
          required>
          <option value="" disabled selected>Selecciona un cliente</option>
          {#each customers as customer}
            <option value={customer.customerId}>{customer.name}</option>
          {/each}
        </select>
      </div>
      <div>
        <label for="serviceId">Servicio:</label>
        <select
          id="serviceId"
          bind:value={booking.serviceId}
          required>
          <option value="" disabled selected>Selecciona un servicio</option>
          {#each services as service}
            <option value={service.serviceId}>{service.serviceName}</option>
          {/each}
        </select>
      </div>
      <div class="button-group">
        <button type="submit">Actualizar</button>
        <button type="button" on:click={() => goto('/bookings')}>Regresar</button>
      </div>
    </form>
  {:else if booking === null}
    <p>Cargando datos...</p>
  {:else}
    <p>Error al cargar los datos. Inténtelo nuevamente.</p>
  {/if}
</div>
