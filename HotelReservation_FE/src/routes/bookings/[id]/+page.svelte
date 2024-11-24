<script lang="ts">
    import { getBookingById } from '$lib/api/booking';
    import type { Booking } from '$lib/api/interfaces/booking';
  import { onMount } from 'svelte';
  
    export let params: { id: string };
    let booking: Booking | null = null;
  
    onMount(async () => {
      try {
        booking = await getBookingById(parseInt(params.id));
      } catch (error) {
        console.error('Error loading booking:', error);
      }
    });
  </script>
  
  <h1>Booking Details</h1>
  
  {#if booking}
    <p>Reservation Date: {booking.reservationDate}</p>
    <p>People Number: {booking.peopleNumber}</p>
    <p>Status: {booking.estado}</p>
    <p>Customer ID: {booking.customerId}</p>
    <p>Service ID: {booking.serviceId}</p>
  {:else}
    <p>Loading...</p>
  {/if}
  