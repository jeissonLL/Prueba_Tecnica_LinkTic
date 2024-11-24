<script lang="ts">
    import { getCustomers, deleteCustomer } from '$lib/api/customer';
    import type { Customer } from '$lib/api/interfaces/customer';
    import { onMount } from 'svelte';
    import { goto } from '$app/navigation';
  
    let customers: Customer[] = [];
    let paginatedCustomers: Customer[] = [];
    let currentPage = 1;
    let itemsPerPage = 5;
    let successMessage = '';
    let errorMessage = '';
  
    const loadCustomers = async () => {
      try {
        customers = await getCustomers();
        paginate(currentPage);
        successMessage = 'Clientes cargados con éxito.';
      } catch (error) {
        errorMessage = 'Error al cargar los clientes.';
        console.error(error);
      }
    };
  
    const paginate = (page: number) => {
      const startIndex = (page - 1) * itemsPerPage;
      const endIndex = page * itemsPerPage;
      paginatedCustomers = customers.slice(startIndex, endIndex);
      currentPage = page;
    };
  
    const totalPages = () => Math.ceil(customers.length / itemsPerPage);
  
    const removeCustomer = async (id: number) => {
      if (confirm('¿Seguro que deseas eliminar este cliente?')) {
        try {
          await deleteCustomer(id); 
          successMessage = 'Cliente eliminado con éxito.';
          await loadCustomers();
        } catch (error) {
          errorMessage = 'Error al eliminar el cliente.';
          console.error(error);
        }
      }
    };
  
    onMount(() => {
      loadCustomers();
    });
  </script>
  
  <h1>Clientes</h1>
  
  <!-- Mensajes de éxito y error -->
  {#if successMessage}
    <div class="message success">{successMessage}</div>
  {/if}
  {#if errorMessage}
    <div class="message error">{errorMessage}</div>
  {/if}
  
  <!-- Botones de acciones -->
  <div class="button-group">
    <button on:click={() => goto('/customers/create')}>Crear Cliente</button>
    <button on:click={() => goto('/bookings')}>Regresar</button>
  </div>
  
  <!-- Tabla de clientes -->
  <table>
    <thead>
      <tr>
        <th>Nombre</th>
        <th>Email</th>
        <th>Acciones</th>
      </tr>
    </thead>
    <tbody>
      {#each paginatedCustomers as customer}
        <tr>
          <td>{customer.name}</td>
          <td>{customer.email}</td>
          <td>
            <button on:click={() => goto(`/customers/${customer.customerId}`)}>Ver</button>
            <button on:click={() => goto(`/customers/update${customer.customerId}`)}>Actualizar</button>
            <button on:click={() => customer.customerId && removeCustomer(customer.customerId)}>Eliminar</button>
          </td>
        </tr>
      {/each}
    </tbody>
  </table>
  
  <!-- Paginación -->
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
  
  <style>
    /* Encabezado principal */
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
      max-width: 800px;
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
  
    /* Tabla */
    table {
      width: 80%;
      border-collapse: collapse;
      margin: 20px auto;
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
  
    table td {
      vertical-align: middle;
    }
  
    table td button {
      padding: 5px 10px;
      font-size: 14px;
      margin: 0 5px;
      border: none;
      border-radius: 4px;
      cursor: pointer;
      background-color: #4caf50;
      color: white;
    }
  
    table td button:nth-child(2) {
      background-color: #2196f3;
    }
  
    table td button:nth-child(3) {
      background-color: #ff5733;
    }
  
    table td button:hover {
      opacity: 0.8;
    }
  
    /* Paginación */
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
  
    /* Estilo responsivo */
    @media (max-width: 768px) {
      table th,
      table td {
        padding: 8px;
      }
  
      .button-group button {
        font-size: 14px;
        padding: 8px 12px;
      }
    }
  </style>
  