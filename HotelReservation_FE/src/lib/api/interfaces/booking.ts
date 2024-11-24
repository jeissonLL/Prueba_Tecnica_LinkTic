export interface Booking {
    bookingId: number;
    reservationDate: string;
    peopleNumber: number;
    estado: string;
    customerId: number;
    serviceId: number;
    customer: {
      name: string;
    };
    services: {
       serviceName : string;
    };
  }