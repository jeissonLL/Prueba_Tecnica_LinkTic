<script lang="ts">
    import { getCustomerById } from '$lib/api/customer';
    import type { Customer } from '$lib/api/interfaces/customer';
    import { onMount } from 'svelte';
    
    export let params: { id: string };
    let customer: Customer | null = null;
    
    onMount(async () => {
      if (params && params.id) {
        customer = await getCustomerById(parseInt(params.id));
      }
    });
  </script>
  
  {#if customer}
    <h1>{customer.name}</h1>
    <p>Email: {customer.email}</p>
  {:else}
    <p>Loading...</p>
  {/if}
  