// +page.ts
export function load({ params }) {
    if (!params.id) {
      throw new Error('Booking ID is required');
    }
  
    //console.log('Received ID:', params.id);  // Verifica que el id es correcto
    return {
      id: params.id,
    };
  }