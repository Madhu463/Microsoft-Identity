// // import { useState } from 'react';
// // import { Link, useNavigate } from "react-router-dom";
// // import axios from 'axios';
// // import { toast } from '@/hooks/use-toast';

// // export const Login = () => {
// //   const [userId, setUserId] = useState(''); 
// //   const [password, setPassword] = useState('');
// //   const [error, setError] = useState('');
// //   const navigate = useNavigate(); 

// //   const handleLogin = async (e) => {
// //     e.preventDefault();
// //     const reqbody = {
// //       "userId": userId,
// //       "password": password
// //     };
  
// //     try {
// //       const response = await axios.post('http://localhost:5233/api/LoginUsers/validate', reqbody);
  
// //       if (response.status === 200) {
// //         const { status, token, role } = response.data;
// //         localStorage.setItem('status', status);
// //         localStorage.setItem('token', token);
// //         localStorage.setItem('role', role);
  
// //         if (role === "Manager") {
// //           navigate('/manager'); // Redirect to manager dashboard
// //         } else {
// //           navigate(`/workers/${role}`); // Redirect to worker's page
// //         }
// //         console.log('User validated successfully');
// //       } else {
// //         console.log('Validation failed');
// //         localStorage.setItem('status', 'failed');
// //       toast({title:' Please check your credentials',variant:'error'});
// //       }
// //     } catch (error) {
// //       console.error('invalid credentials', error);
// //       localStorage.setItem('status', 'failed');
// //       toast({ title: 'Invalid Credentials', variant:  "destructive" });
// //     }
// //   };
  

// //   return (
// //     <div className="relative bg-gray-600 flex h-screen w-full items-center justify-center bg-gradient-to-r">
// //       <img src="/bg-images/bglogin5.jpg" alt="Background" className="absolute inset-0 w-full h-full object-cover" />
// //       <div className="relative backdrop-blur-3xl rounded-lg shadow-lg p-8 w-full max-w-[800px] h-auto md:h-[450px] bg-cover bg-center">
// //         <div className="flex h-full items-center justify-center rounded-lg text-white shadow-lg md:flex-row">
// //           <div className="relative hidden md:block w-1/4 overflow-hidden rounded-l-lg">
// //             <img
// //               src="/bg-images/logins5.jpg"
// //               alt="Login illustration"
// //               className="object-cover invert w-full h-full"
// //               style={{ aspectRatio: '350/450', objectFit: 'cover' }}
// //             />
// //           </div>
// //           <div className="flex w-full md:w-1/2 flex-col justify-center gap-6 p-8 md:p-12">
// //             <div className="space-y-2 border text-white rounded-md p-2">
// //               <h1 className="text-3xl font-bold">Welcome User!</h1>
// //               <p className="text-muted-foreground text-white">Enter your credentials to access your account.</p>
// //             </div>
// //             <form className="space-y-4" onSubmit={handleLogin}>
// //               <div className="space-y-2 font-bold">
// //                 <label htmlFor="userId">UserId</label>
// //                 <input
// //                   id="userId"
// //                   type="text"
// //                   value={userId}
// //                   onChange={(e) => setUserId(e.target.value)}
// //                   required
// //                   className="w-full text-black bg-slate-100 p-2 border rounded"
// //                 />
// //               </div>
// //               <div className="space-y-2">
// //                 <div className="flex items-center justify-between font-bold">
// //                   <label htmlFor="password">Password</label>
// //                   {/* <a
// //                     href="#"
// //                     className="text-sm font-medium underline underline-offset-4 hover:cursor-pointer"
// //                   >
// //                     Forgot password?
// //                   </a> */}
// //                 </div>
// //                 <input
// //                   id="password"
// //                   type="password"
// //                   value={password}
// //                   onChange={(e) => setPassword(e.target.value)}
// //                   required
// //                   className="w-full text-black bg-slate-100 p-2 border rounded"
// //                 />
// //               </div>
// //               <button type="submit" className="w-full border-2 bg-gray-400 border-teal-500 text-black font-bold px-4 py-2 rounded hover:cursor-pointer hover:bg-teal-300 hover:text-black transition ease-in-out duration-300">
// //                 LOG IN
// //               </button>
// //             </form>
// //             {error && <p className="text-red-500 mt-2">{error}</p>}
// //           </div>
// //         </div>
// //       </div>
// //     </div>
// //   );
// // };

// // export default Login;

import React from 'react';
import { useMsal } from '@azure/msal-react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
const Login = () => {
    const { instance } = useMsal();
    const navigate = useNavigate();

    const handleLogin = async () => {
        const loginRequest = {
            scopes: ["email User.Read profile openid"],
        };

        // instance.loginPopup(loginRequest)
        //     .then(response => {
        //         const userEmail = response.account.username;
        instance.loginPopup(loginRequest)
        .then(async response => {
            const userEmail = response.account.username;

            // Retrieve the access token
            const tokenResponse = await instance.acquireTokenSilent({
                scopes: ["api://95b1f998-ee4c-4af5-bdba-bc028bb89fbf/User.Read"],
                account: response.account,
            });

            const accessToken = tokenResponse.accessToken;
            console.log(accessToken)
            // Now you can use the access token for your requests
            axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;
            localStorage.setItem('token', accessToken);

                // Define your user type map
                const userIdToWorkerTypeMap = {
                    'user2@sathvikreddy8685gmail.onmicrosoft.com': 'Worker1',
                    'user3@sathvikreddy8685gmail.onmicrosoft.com': 'Worker2',
                    'user4@sathvikreddy8685gmail.onmicrosoft.com': 'Worker3',
                    'user5@sathvikreddy8685gmail.onmicrosoft.com': 'Worker4',
                    'user1@sathvikreddy8685gmail.onmicrosoft.com': 'manager', // Manager's email
                };

                const userType = userIdToWorkerTypeMap[userEmail];
                console.log('Navigating to:', userType ? `/workers/${userType}` : '/manager');

                if (userType) {
                  // Navigate based on the user type
                  if (userType === 'manager') {
                      navigate('/manager');
                  } else if (userType === 'Worker1') {
                      navigate('/workers/Worker1');
                  } else if (userType === 'Worker2') {
                      navigate('/workers/Worker2');
                  } else if (userType === 'Worker3') {
                      navigate('/workers/Worker3');
                  } else if (userType === 'Worker4') {
                      navigate('/workers/Worker4');
                  } else {
                      // Handle unexpected user types
                      alert("Unexpected user type.");
                  }
                } else {
                    // Handle unauthorized users
                    alert("You do not have access to this application.");
                }
            })
            .catch(err => {
                console.error(err);
                alert("Login failed.");
            });
    };

    return (
     
            <div className="relative bg-gray-600 flex h-screen w-full items-center justify-center bg-gradient-to-r">
              <img src="/bg-images/bglogin5.jpg" alt="Background" className="absolute inset-0 w-full h-full object-cover" />
              <div className="relative backdrop-blur-3xl rounded-lg shadow-lg p-8 w-full max-w-[800px] h-auto md:h-[450px] bg-cover bg-center">
                <div className="flex h-full items-center justify-center rounded-lg text-white shadow-lg md:flex-row">
                  <div className="relative hidden md:block w-1/4 overflow-hidden rounded-l-lg">
                    <img
                      src="/bg-images/logins5.jpg"
                      alt="Login illustration"
                      className="object-cover invert w-full h-full"
                      style={{ aspectRatio: '350/450', objectFit: 'cover' }}
                    />
                  </div>
                  <div className="flex w-full md:w-1/2 flex-col justify-center gap-6 p-8 md:p-12">
                    <div className="space-y-2 border text-white rounded-md p-2">
                      <h1 className="text-3xl font-bold">Welcome User!</h1>
                      <p className="text-muted-foreground text-white">Enter your credentials to access your account.</p>
                    </div>
                    <button 
                    onClick={handleLogin}
                   className="w-full bg-blue-600 text-white font-bold py-2 rounded hover:bg-blue-500 transition duration-300"
                >                   Login with Microsoft              </button> 
                  </div>
                </div>
              </div>
            </div>
    );
};

export default Login;
// import React from 'react';
// import { useMsal } from '@azure/msal-react';
// import { useNavigate } from 'react-router-dom';
// import axios from 'axios';

// const Login = () => {
//     const { instance } = useMsal();
//     const navigate = useNavigate();

//     const handleLogin = async () => {
//         const loginRequest = {
//             scopes: ["email User.Read profile openid"],
//         };

//         instance.loginPopup(loginRequest)
//             .then(async response => {
//                 const accessToken = await instance.acquireTokenSilent({
//                     scopes: ["api://95b1f998-ee4c-4af5-bdba-bc028bb89fbf/User.Read"],
//                     account: response.account,
//                 });

//                 // Set the Authorization header for axios
//                 axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken.accessToken}`;
//                 localStorage.setItem('token', accessToken);

//                 const userEmail = response.account.username;

//                 // Define your user type map
//                 const userIdToWorkerTypeMap = {
//                     'user2@sathvikreddy8685gmail.onmicrosoft.com': 'Worker1',
//                     'user3@sathvikreddy8685gmail.onmicrosoft.com': 'Worker2',
//                     'user4@sathvikreddy8685gmail.onmicrosoft.com': 'Worker3',
//                     'user5@sathvikreddy8685gmail.onmicrosoft.com': 'Worker4',
//                     'user1@sathvikreddy8685gmail.onmicrosoft.com': 'manager', // Manager's email
//                 };

//                 const userType = userIdToWorkerTypeMap[userEmail];

//                 // Navigate based on the user type
//                 if (userType === 'manager') {
//                     navigate('/manager');
//                 } else if (userType) {
//                     navigate(`/workers/${userType}`);
//                 } else {
//                     alert("You do not have access to this application.");
//                 }
//             })
//             .catch(err => {
//                 console.error(err);
//                 alert("Login failed.");
//             });
//     };

//     return (
//         <div className="relative bg-gray-600 flex h-screen w-full items-center justify-center bg-gradient-to-r">
//             <img 
//                 src="/bg-images/bglogin5.jpg" 
//                 alt="Background" 
//                 className="absolute inset-0 w-full h-full object-cover" 
//             />
//             <div className="relative backdrop-blur-3xl rounded-lg shadow-lg p-8 w-full max-w-sm h-auto bg-white bg-opacity-70">
//                 <h2 className="text-3xl font-bold text-center mb-4">Welcome!</h2>
//                 <p className="text-center mb-6">Please log in using your Microsoft account.</p>
//                 <button 
//                     onClick={handleLogin}
//                     className="w-full bg-blue-600 text-white font-bold py-2 rounded hover:bg-blue-500 transition duration-300"
//                 >
//                     Login with Microsoft
//                 </button>
//             </div>
//         </div>
//     );
// };

// export default Login;

