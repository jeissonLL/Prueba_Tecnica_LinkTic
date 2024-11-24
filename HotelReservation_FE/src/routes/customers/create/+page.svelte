<script lang="ts">
    import { createCustomer } from '$lib/api/customer';
    import { goto } from '$app/navigation';
  
    let name = '';
    let email = '';
    let successMessage = '';
    let errorMessage = '';
  
    const create = async () => {
      successMessage = '';
      errorMessage = '';
      try {
        await createCustomer({ name, email });
        successMessage = 'Cliente creado con éxito.';
        setTimeout(() => {
          goto('/customers');
        }, 2000);
      } catch (error) {
        errorMessage = 'Hubo un error al crear el cliente. Por favor, intenta de nuevo.';
        console.error(error);
      }
    };
  
    const goBack = () => {
      goto('/customers');
    };
  </script>
  
  <h1>Crear Cliente</h1>
  
  <!-- Mensajes de éxito y error -->
  {#if successMessage}
    <div class="message success">{successMessage}</div>
  {/if}
  {#if errorMessage}
    <div class="message error">{errorMessage}</div>
  {/if}
  
  <form on:submit|preventDefault={create}>
    <div class="form-group">
      <label for="name">Nombre:</label>
      <input id="name" type="text" bind:value={name} required />
    </div>
  
    <div class="form-group">
      <label for="email">Correo Electrónico:</label>
      <input id="email" type="email" bind:value={email} required />
    </div>
  
    <div class="button-group">
      <button type="submit">Crear Cliente</button>
      <button type="button" on:click={goBack}>Regresar</button>
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
  
    input {
      width: 100%;
      padding: 8px;
      border: 1px solid #ddd;
      border-radius: 4px;
      font-size: 16px;
      box-sizing: border-box;
    }
  
    input:focus {
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
  