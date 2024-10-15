import React, { useState } from 'react';

const ContactForm = () => {
  const [mapHeight, setMapHeight] = useState(500);

  const handleHeightChange = (event) => {
    setMapHeight(event.target.value);
  };

  return (
    <div>
      <nav className="bg-gray-800 p-4 flex justify-between items-center">
      <h1 className="text-3xl font-bold text-teal-400 p-1 text-center">Contact Us</h1>
      <div className="flex space-x-4">
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" className="size-6 text-teal-400">
            <path fillRule="evenodd" d="M1.5 4.5a3 3 0 0 1 3-3h1.372c.86 0 1.61.586 1.819 1.42l1.105 4.423a1.875 1.875 0 0 1-.694 1.955l-1.293.97c-.135.101-.164.249-.126.352a11.285 11.285 0 0 0 6.697 6.697c.103.038.25.009.352-.126l.97-1.293a1.875 1.875 0 0 1 1.955-.694l4.423 1.105c.834.209 1.42.959 1.42 1.82V19.5a3 3 0 0 1-3 3h-2.25C8.552 22.5 1.5 15.448 1.5 6.75V4.5Z" clipRule="evenodd" />
          </svg>
          <p className='text-teal-400'>MON-FRI 8:00AM-6.00PM</p>
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" className="size-6 text-teal-400">
            <path fillRule="evenodd" d="m11.54 22.351.07.04.028.016a.76.76 0 0 0 .723 0l.028-.015.071-.041a16.975 16.975 0 0 0 1.144-.742 19.58 19.58 0 0 0 2.683-2.282c1.944-1.99 3.963-4.98 3.963-8.827a8.25 8.25 0 0 0-16.5 0c0 3.846 2.02 6.837 3.963 8.827a19.58 19.58 0 0 0 2.682 2.282 16.975 16.975 0 0 0 1.145.742ZM12 13.5a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z" clipRule="evenodd" />
          </svg>
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" className="size-6 text-teal-400">
            <path fillRule="evenodd" d="M12 2.25c-5.385 0-9.75 4.365-9.75 9.75s4.365 9.75 9.75 9.75 9.75-4.365 9.75-9.75S17.385 2.25 12 2.25ZM12.75 6a.75.75 0 0 0-1.5 0v6c0 .414.336.75.75.75h4.5a.75.75 0 0 0 0-1.5h-3.75V6Z" clipRule="evenodd" />
          </svg>
        </div>
      </nav>
      <p className="md:w-2/3 p-4 font-medium italic text-gray-800" 
        
      >At WHEEL FACTORY, we are committed to excellence in every aspect of our wheel 
        manufacturing process. From the initial design to the final product, our focus on 
        quality and performance guarantees that our wheels are built to last. 
        "Experience the perfect blend of strength and elegance with our expertly crafted wheels"</p>
        <div className="flex items-center p-4">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" className="w-8 h-8 text-teal-400">
          <path fillRule="evenodd" d="M12 2.25c-5.385 0-9.75 4.365-9.75 9.75s4.365 9.75 9.75 9.75 9.75-4.365 9.75-9.75S17.385 2.25 12 2.25Zm4.28 10.28a.75.75 0 0 0 0-1.06l-3-3a.75.75 0 1 0-1.06 1.06l1.72 1.72H8.25a.75.75 0 0 0 0 1.5h5.69l-1.72 1.72a.75.75 0 1 0 1.06 1.06l3-3Z" clipRule="evenodd" />
        </svg>
        <p className="md:w-2/3  font-bold underline italic text-gray-800">LOCATE US ON MAPS</p>
        <button className='bg-gray-800 text-teal-400 font-bold  text-center rounded p-2 w-60 h-40px'>CONTACT</button>
      </div>
      <div className="flex flex-col md:flex-row p-4">
        
        <div className="md:w-2/3 p-4">
          <div style={{ width: '100%' }}>
            <iframe
              width="100%"
              height={mapHeight}
              frameBorder="0"
              scrolling="no"
              marginHeight="0"
              marginWidth="0"
              src="https://maps.google.com/maps?width=720&height=600&hl=en&q=WHEEL%20INDIA%20LIMITED+(My%20Business%20Name)&t=&z=13&ie=UTF8&iwloc=B&output=embed"
            >
              <a href="https://www.gps.ie/">gps tracker sport</a>
            </iframe>
          </div>
          <input
            type="range"
            min="300"
            max="1000"
            value={mapHeight}
            onChange={handleHeightChange}
            className="mt-4"
          />
        </div>
        
        <div className="md:w-1/3 p-4 bg-gray-100 rounded-md">
          <h2 className="text-lg font-bold mb-4">Contact Details</h2>
          <p className="mb-4">
            <strong className="font-semibold text-gray-700">Registered Office:</strong><br />
            <span className="text-gray-600">E-Wing, A Block, 14th Floor, Unit No-3, Trade Link, Kamala City, Kamala Mills Compound, Senapati Bapat Marg, Lower Parel West, Mumbai, Maharashtra 400013</span>
          </p>
          <p className="mb-4">
            <strong className="font-semibold text-gray-700">Manufacturing Unit:</strong><br />
            <span className="text-gray-600">B-10/1, MIDC, Malegaon, Sinner, Nasik-422113</span>
          </p>
          <p className="mb-4">
            <strong className="font-semibold text-gray-700">Phone:</strong><br />
            <span className="text-gray-600">+91 8976033830<br />+91 8976033831</span>
          </p>
          <p className="mb-4">
            <strong className="font-semibold text-gray-700">Customer Service:</strong><br />
            <span className="text-gray-600">+91-9137450920<br />+91-8169667056</span>
          </p>
          <p className="mb-4">
            <strong className="font-semibold text-gray-700">Email:</strong><br />
            <span className="text-gray-600">sales@neowheels.com<br />media@neowheels.com</span>
          </p>
        </div>
      </div>
      <footer className="w-full border-t-4 border-teal-400 pt-8 pb-12 bg-gray-800">
        <div className="container max-w-6xl mx-auto flex flex-col sm:flex-row justify-between items-center gap-8">
          <div className="text-center sm:text-left">
            <h4 className="text-xl font-bold text-teal-400 ">Wheel Factory</h4>
            <p className="text-white text-lg">
              Remanufacturing quality products since 1985.
            </p>
          </div>
          <div className="grid grid-cols-1 sm:grid-cols-3 gap-8 text-center sm:text-left">
            <div>
              <h5 className="text-xl font-bold text-teal-400 mb-2">Partners</h5>
              <ul className="space-y-1 text-s text-white">
                <li>KIA</li>
                <li>HONDA</li>
                <li>TATA</li>
                <li>BMW</li>
                <li>MERCEDES</li>
              </ul>
            </div>
            <div>
              <h5 className="text-xl font-bold text-teal-400 mb-2">
                Contact Us
              </h5>
              <ul className="space-y-1 text-lg text-white font-sans">
                <li>Email: contact@wheelfactory.com</li>
                <li>Phone: +1 (123) 456-7890</li>
              </ul>
            </div>
            <div>
              <h5 className="text-xl font-bold text-teal-400 mb-2">
                Head Office
              </h5>
              <p className="space-y-1 text-lg text-white">
                123 Main Street
                <br />
                Anytown, USA 12345
                <br />
                Open: Mon-Fri, 9 AM - 5 PM
              </p>
            </div>
          </div>
        </div>
      </footer>
    </div>
  );
};

export default ContactForm;
