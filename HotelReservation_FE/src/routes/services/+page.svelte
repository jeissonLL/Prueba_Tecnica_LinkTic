<script lang="ts">
    import { onMount } from 'svelte';
    import { getService } from '$lib/api/service';
    import type { Service } from '$lib/api/interfaces/service';
    import { goto } from '$app/navigation';
  
    let services: Service[] = [];
  
    const loadServices = async () => {
      try {
        services = await getService();
      } catch (error) {
        console.error('Error al cargar los servicios:', error);
      }
    };
  
    const removeService = async (id: number) => {
      if (confirm('¿Seguro que deseas eliminar este servicio?')) {
        try {
          await loadServices();
        } catch (error) {
          console.error('Error al eliminar el servicio:', error);
        }
      }
    };
  
    onMount(loadServices);
  </script>
  
  <h1>Servicios</h1>
  <div class="button-group">
    <button on:click={() => goto('/services/create')}>Crear Servicio</button>
    <button on:click={() => goto('/bookings')}>Regresar</button>
  </div>
  
  <table>
    <thead>
      <tr>
        <th>Nombre</th>
        <th>Descripción</th>
        <th>Precio</th>
        <th>Acciones</th>
      </tr>
    </thead>
    <tbody>
      {#each services as service}
        <tr>
          <td>{service.serviceName}</td>
          <td>{service.serviceDescription}</td>
          <td>${service.price}</td>
          <td>
            <button on:click={() => goto(`/services/${service.serviceId}/view`)}>Ver</button>
            <button on:click={() => goto(`/services/${service.serviceId}/edit`)}>Editar</button>
          </td>
        </tr>
      {/each}
    </tbody>
  </table>
  
  <style>
    /* Encabezado principal */
    h1 {
      text-align: center;
      font-size: 2rem;
      margin-top: 20px;
      color: #333;
    }
  
    /* Grupo de botones */
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
      width: 100%;
      border-collapse: collapse;
      margin: 20px auto;
      max-width: 1200px;
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
  
    table td button:hover {
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
  