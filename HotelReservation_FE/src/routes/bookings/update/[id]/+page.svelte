<script lang="ts">
    import { updateBooking, getBookingById } from '$lib/api/booking';
    import { goto } from '$app/navigation';
    import type { Booking } from '$lib/api/interfaces/booking';
  import { onMount } from 'svelte';
  
    export let params: { id: string };
    let booking: Booking | null = null;
  
    const loadBooking = async () => {
      try {
        booking = await getBookingById(parseInt(params.id));
      } catch (error) {
        console.error('Error loading booking:', error);
      }
    };
  
    const update = async () => {
      if (booking) {
        try {
          await updateBooking(booking);
          goto('/bookings');
        } catch (error) {
          console.error('Error updating booking:', error);
        }
      }
    };
  
    onMount(loadBooking);
  </script>
  
  <h1>Update Booking</h1>
  {#if booking}
    <form on:submit|preventDefault={update}>
      <input type="date" bind:value={booking.reservationDate} required />
      <input type="number" bind:value={booking.peopleNumber} required />
      <input type="text" bind:value={booking.estado} required />
      <input type="number" bind:value={booking.customerId} required />
      <input type="number" bind:value={booking.serviceId} required />
      <button type="submit">Update</button>
    </form>
  {:else}
    <p>Loading...</p>
  {/if}
  