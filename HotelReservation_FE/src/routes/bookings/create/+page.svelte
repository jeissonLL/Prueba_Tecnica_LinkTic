<script lang="ts">
    import { createBooking } from '$lib/api/booking';
    import { getCustomers } from '$lib/api/customer';
    import { getService } from '$lib/api/service';
    import { goto } from '$app/navigation';
    import type { Booking } from '$lib/api/interfaces/booking';
    import type { Customer } from '$lib/api/interfaces/customer';
    import type { Service } from '$lib/api/interfaces/service';
  
    let reservationDate = ''; // Fecha de la reserva
    let peopleNumber = 1; // Número de personas
    let estado = ''; // Estado de la reserva
    let customerId = 0; // ID del cliente asociado
    let serviceId = 0; // ID del servicio asociado
  
    let customers: Customer[] = [];
    let services: Service[] = [];
    let successMessage = '';
    let errorMessage = '';
  
    const loadData = async () => {
      try {
        customers = await getCustomers();
        services = await getService();
      } catch (error) {
        console.error('Error loading customers or services:', error);
      }
    };
  
    const validateDate = () => {
      const selectedDate = new Date(reservationDate).getTime();
      const now = new Date().getTime();
      const todayStart = new Date();
      todayStart.setHours(0, 0, 0, 0);
      const todayStartTimestamp = todayStart.getTime();
  
      if (selectedDate < todayStartTimestamp) {
        errorMessage = 'La fecha seleccionada no puede ser en el pasado.';
        return false;
      }
  
      if (selectedDate >= todayStartTimestamp && selectedDate <= now + 2 * 60 * 60 * 1000) {
        errorMessage =
          'La reserva debe realizarse con al menos 2 horas de anticipación para el día actual.';
        return false;
      }
  
      return true;
    };
  
    const create = async () => {
      successMessage = '';
      errorMessage = '';
  
      if (!validateDate()) return;
  
      const booking: Booking = {
        reservationDate,
        peopleNumber,
        estado,
        customerId,
        serviceId,
        bookingId: 0,
        customer: { name: '' },
        services: { serviceName: '' },
      };
  
      try {
        await createBooking(booking);
        successMessage = 'Reserva creada con éxito.';
        setTimeout(() => goto('/bookings'), 2000);
      } catch (error) {
        errorMessage = 'Error al crear la reserva. Por favor, intenta nuevamente.';
        console.error(error);
      }
    };
  
    loadData();
  </script>
  
  <h1>Crear Reserva</h1>
  
  <!-- Mensajes de éxito y error -->
  {#if successMessage}
    <div class="message success">{successMessage}</div>
  {/if}
  {#if errorMessage}
    <div class="message error">{errorMessage}</div>
  {/if}
  
  <form on:submit|preventDefault={create}>
    <div class="form-group">
      <label for="reservationDate">Fecha y Hora de la Reserva:</label>
      <input
        id="reservationDate"
        type="datetime-local"
        bind:value={reservationDate}
        required
        min={new Date().toISOString().slice(0, 16)} />
    </div>
  
    <div class="form-group">
      <label for="peopleNumber">Número de Personas:</label>
      <input id="peopleNumber" type="number" bind:value={peopleNumber} required min="1" />
    </div>
  
    <div class="form-group">
      <label for="estado">Estado:</label>
      <input id="estado" type="text" bind:value={estado} required />
    </div>
  
    <div class="form-group">
      <label for="customerId">Cliente:</label>
      <select id="customerId" bind:value={customerId} required>
        <option value="" disabled selected>Selecciona un cliente</option>
        {#each customers as customer}
          <option value={customer.customerId}>{customer.name}</option>
        {/each}
      </select>
    </div>
  
    <div class="form-group">
      <label for="serviceId">Servicio:</label>
      <select id="serviceId" bind:value={serviceId} required>
        <option value="" disabled selected>Selecciona un servicio</option>
        {#each services as service}
          <option value={service.serviceId}>{service.serviceName}</option>
        {/each}
      </select>
    </div>
  
    <div class="button-group">
      <button type="submit">Crear Reserva</button>
      <button type="button" on:click={() => goto('/bookings')}>Regresar</button>
    </div>
  </form>
  
  <style>
    /* Encabezado */
    h1 {
      text-align: center;
      font-size: 2rem;
      margin-top: 20px;
      color: #333;
    }
  
    /* Mensajes */
    .message {
      text-align: center;
      padding: 10px;
      margin: 10px auto;
      max-width: 600px;
      border-radius: 4px;
      font-size: 16px;
    }
  
    .message.success {
      background-color: #d4edda;
      color: #155724;
      border: 1px solid #c3e6cb;
    }
  
    .message.error {
      background-color: #f8d7da;
      color: #721c24;
      border: 1px solid #f5c6cb;
    }
  
    /* Formulario */
    form {
      max-width: 500px;
      margin: 20px auto;
      padding: 20px;
      border: 1px solid #ddd;
      border-radius: 4px;
      background-color: #f9f9f9;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }
  
    .form-group {
      margin-bottom: 15px;
    }
  
    label {
      display: block;
      margin-bottom: 5px;
      font-weight: bold;
      color: #333;
    }
  
    input, select {
      width: 100%;
      padding: 8px;
      border: 1px solid #ddd;
      border-radius: 4px;
      font-size: 16px;
      box-sizing: border-box;
    }
  
    input:focus, select:focus {
      outline: none;
      border-color: #007bff;
      box-shadow: 0 0 3px rgba(0, 123, 255, 0.25);
    }
  
    /* Botones */
    .button-group {
      text-align: center;
      margin-top: 20px;
    }
  
    button {
      padding: 10px 15px;
      font-size: 16px;
      margin: 0 10px;
      border: none;
      border-radius: 4px;
      cursor: pointer;
      background-color: #007bff;
      color: white;
      transition: background-color 0.3s;
    }
  
    button:hover {
      background-color: #0056b3;
    }
  
    button[type='button'] {
      background-color: #6c757d;
    }
  
    button[type='button']:hover {
      background-color: #5a6268;
    }
  
    /* Estilo responsivo */
    @media (max-width: 768px) {
      form {
        padding: 15px;
      }
  
      button {
        font-size: 14px;
        padding: 8px 12px;
      }
    }
  </style>
  