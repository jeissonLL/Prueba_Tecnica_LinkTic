<script lang="ts">
    import { getBookings, deleteBooking } from '$lib/api/booking';
    import { getCustomers } from '$lib/api/customer';
    import { getService } from '$lib/api/service';
    import { goto } from '$app/navigation';
    import type { Booking } from '$lib/api/interfaces/booking';
    import type { Customer } from '$lib/api/interfaces/customer';
    import type { Service } from '$lib/api/interfaces/service';
    import { onMount } from 'svelte';
    import * as XLSX from 'xlsx';
  
    let bookings: Booking[] = [];
    let customers: Customer[] = [];
    let services: Service[] = [];
    let combinedData: { booking: Booking; customer: Customer | null; service: Service | null }[] = [];
    let filteredData: typeof combinedData = [];
    let searchQuery = '';
    let currentPage = 1;
    let itemsPerPage = 5;
  
    let successMessage = '';
    let errorMessage = '';
  
    const loadBookings = async () => {
      try {
        bookings = await getBookings();
      } catch (error) {
        console.error('Error loading bookings:', error);
      }
    };
  
    const loadCustomers = async () => {
      try {
        customers = await getCustomers();
      } catch (error) {
        console.error('Error loading customers:', error);
      }
    };
  
    const loadServices = async () => {
      try {
        services = await getService();
      } catch (error) {
        console.error('Error loading services:', error);
      }
    };
  
    const combineBookingsAndCustomers = () => {
      combinedData = bookings.map((booking) => {
        const customer = customers.find((c) => c.customerId === booking.customerId) || null;
        const service = services.find((s) => s.serviceId === booking.serviceId) || null;
        return { booking, customer, service };
      });
      filterData();
    };
  
    const filterData = () => {
      filteredData = combinedData.filter(({ customer }) =>
        customer?.name?.toLowerCase().includes(searchQuery.toLowerCase())
      );
      displayPaginatedData();
    };
  
    const paginate = (page: number) => {
      currentPage = page;
      displayPaginatedData();
    };
  
    const totalPages = () => {
      return Math.max(Math.ceil(filteredData.length / itemsPerPage), 1);
    };
  
    let paginatedData: { booking: Booking; customer: Customer | null; service: Service | null }[] = [];
    const displayPaginatedData = () => {
      const startIndex = (currentPage - 1) * itemsPerPage;
      const endIndex = currentPage * itemsPerPage;
      paginatedData = filteredData.slice(startIndex, endIndex);
    };
  
    const cancelBooking = async (bookingId: number) => {
      successMessage = '';
      errorMessage = '';
      try {
        await deleteBooking(bookingId); 
        successMessage = 'Reserva cancelada exitosamente.';
        await loadBookings(); 
        combineBookingsAndCustomers();
        displayPaginatedData();
      } catch (error) {
        errorMessage = 'Error al cancelar la reserva. Intente nuevamente.';
        console.error(error);
      }
    };
  
    const exportToExcel = () => {
      // Prepara los datos para exportar
      const exportData = filteredData.map(({ booking, customer, service }) => ({
        'Fecha de Reserva': new Date(booking.reservationDate).toLocaleDateString(),
        'Número de Personas': booking.peopleNumber,
        Estado: booking.estado,
        'Nombre del Cliente': customer?.name || 'Sin asignar',
        'Nombre del Servicio': service?.serviceName || 'Sin asignar',
      }));
      
      const wb = XLSX.utils.book_new();
      const ws = XLSX.utils.json_to_sheet(exportData);
      XLSX.utils.book_append_sheet(wb, ws, 'Reservas');
      XLSX.writeFile(wb, 'reservas.xlsx');
    };
  
    onMount(async () => {
      await Promise.all([loadBookings(), loadCustomers(), loadServices()]);
      combineBookingsAndCustomers();
      displayPaginatedData();
    });
</script>

<div class="messages">
    {#if successMessage}
      <div class="message success">{successMessage}</div>
    {/if}
    {#if errorMessage}
      <div class="message error">{errorMessage}</div>
    {/if}
</div>

<div class="navbar">
    <ul>
      <!-- svelte-ignore a11y_invalid_attribute -->
      <li><a href="#" on:click={() => goto('/bookings')}>Reserva</a></li>
      <!-- svelte-ignore a11y_invalid_attribute -->
      <li><a href="#" on:click={() => goto('/customers')}>Cliente</a></li>
      <!-- svelte-ignore a11y_invalid_attribute -->
      <li><a href="#" on:click={() => goto('/services')}>Servicio</a></li>
    </ul>
</div>

<div class="container">
    <div class="search">
        <input
            type="text"
            placeholder="Buscar por nombre del cliente"
            bind:value={searchQuery}
            on:input={() => {
                filterData();
                displayPaginatedData();
            }}
        />
    </div>
    
    <div class="button-group">
        <button on:click={() => goto('/bookings/create')}>Crear reserva</button>
        <button on:click={exportToExcel}>Exportar a Excel</button>
    </div>

    <table>
        <thead>
            <tr>
                <th>Fecha de Reserva</th>
                <th>Número de Personas</th>
                <th>Estado</th>
                <th>Nombre del Cliente</th>
                <th>Nombre del Servicio</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            {#each paginatedData as { booking, customer, service }}
            <tr>
                <td>{new Date(booking.reservationDate).toLocaleDateString()}</td>
                <td>{booking.peopleNumber}</td>
                <td>{booking.estado}</td>
                <td>{customer?.name || 'Sin asignar'}</td>
                <td>{service?.serviceName || 'Sin asignar'}</td>
                <td class="actions">
                    <button on:click={() => goto(`/bookings/${booking.bookingId}`)}>Ver</button>
                    <button on:click={() => goto(`/bookings/update/${booking.bookingId}`)}>Actualizar</button>
                    <button on:click={() => cancelBooking(booking.bookingId)}>Cancelar Reserva</button>
                </td>
            </tr>
            {/each}
        </tbody>
    </table>
    
    <div class="pagination">
        {#each Array(totalPages()).fill(0).map((_, i) => i + 1) as page}
        <button
            on:click={() => paginate(page)}
            class:selected={page === currentPage}
        >
            {page}
        </button>
        {/each}
    </div>
</div>
  
  <style>
    /* Estilos del menú de navegación */
    .navbar {
      background-color: #333;
      color: white;
      padding: 10px 0;
      text-align: center;
    }
  
    .navbar ul {
      list-style-type: none;
      margin: 0;
      padding: 0;
    }
  
    .navbar li {
      display: inline;
      margin-right: 20px;
    }
  
    .navbar a {
      color: white;
      text-decoration: none;
      font-size: 18px;
      padding: 8px 16px;
      border-radius: 4px;
      transition: background-color 0.3s;
    }
  
    .navbar a:hover {
      background-color: #444;
    }
  
    /* Contenedor general */
    .container {
      max-width: 1200px;
      margin: 20px auto;
      padding: 20px;
    }
  
    /* Estilos del input de búsqueda */
    .search {
      text-align: center;
      margin-bottom: 20px;
    }
  
    .search input {
      padding: 8px;
      font-size: 16px;
      width: 50%;
      max-width: 400px;
      border: 1px solid #ccc;
      border-radius: 4px;
    }
  
    /* Estilos de la tabla */
    table {
      width: 100%;
      border-collapse: collapse;
      margin-top: 20px;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }
  
    table th,
    table td {
      padding: 12px 15px;
      text-align: left;
      border: 1px solid #ddd;
    }
  
    table th {
      background-color: #f4f4f4;
      font-weight: bold;
    }
  
    table td.actions {
      text-align: center;
    }
  
    table button {
      padding: 5px 10px;
      font-size: 14px;
      margin: 0 5px;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }
  
    table button:hover {
      opacity: 0.8;
    }
  
    table button:nth-child(1) {
      background-color: #4caf50;
      color: white;
    }
  
    table button:nth-child(2) {
      background-color: #2196f3;
      color: white;
    }
  
    table button:nth-child(3) {
      background-color: #ff9800;
      color: white;
    }
  
    /* Estilos de la paginación */
    .pagination {
      text-align: center;
      margin-top: 20px;
    }
  
    .pagination button {
      padding: 8px 15px;
      margin: 0 5px;
      border: 1px solid #ddd;
      border-radius: 4px;
      cursor: pointer;
      background-color: #f4f4f4;
      font-size: 16px;
    }
  
    .pagination button.selected {
      background-color: #007bff;
      color: white;
      font-weight: bold;
    }
  
    .pagination button:hover {
      opacity: 0.8;
    }
  
    /* Estilo responsive */
    @media (max-width: 768px) {
      .search input {
        width: 80%;
      }
  
      table th,
      table td {
        padding: 8px;
      }
    }

    .messages {
    margin-bottom: 10px;
  }

  .message.success {
    color: green;
  }

  .message.error {
    color: red;
  }

/* Botones */
    .button-group {
        text-align: center;
        margin: 20px 0;
    }

    .button-group button {
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

    .button-group button:hover {
        background-color: #0056b3;
    }

    .button-group button {
        font-size: 14px;
        padding: 8px 12px;
      }
  </style>
  