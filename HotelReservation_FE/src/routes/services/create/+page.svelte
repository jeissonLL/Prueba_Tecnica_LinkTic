<script lang="ts">
    import { createService } from '$lib/api/service';
    import { goto } from '$app/navigation';
    import type { Service } from '$lib/api/interfaces/service';
  
    // Campos de la reserva
    let serviceName = ''; 
    let serviceDescription = '';
    let price = 0;
  
    const create = async () => {
      const service: Service = {
        serviceName, serviceDescription, price
      };
      try {
        await createService(service);
        goto('/Service');
      } catch (error) {
        console.error('Error creating service:', error);
      }
    };
</script>

<h1>Crear Servicio</h1>
<form on:submit|preventDefault={create}>
    <label>
        Nombre:
        <input bind:value={serviceName} required />
    </label>
    <label>
        Descripción:
        <textarea bind:value={serviceDescription} required></textarea>
    </label>
    <label>
        Precio:
        <input type="number" bind:value={price} required />
    </label>
    <button type="submit"  on:click={() => goto('/services')}>Guardar</button>
    <button type="button" on:click={() => goto('/services')}>Cancelar</button>
</form>
