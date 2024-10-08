// import React, { useEffect, useState } from 'react';
// import { Navigate, Outlet } from 'react-router-dom';

// const ProtectedRoute = () => {
//     const [isAuthenticated, setIsAuthenticated] = useState(null); // Initially set to null (loading state)

//     useEffect(() => {
//         const status = localStorage.getItem('status');
//         setIsAuthenticated(status === 'Success'); // Compare status with 'Success'
//     }, []);

//     if (isAuthenticated === null) {
//         // Show a loading spinner or message while authentication is being checked
//         return <div>Auth service is working </div>;
//     }

//     return isAuthenticated ? <Outlet /> : <Navigate to="/login" />;
// };

// export default ProtectedRoute;
// import React, { useEffect, useState } from 'react';
// import { Navigate, Outlet } from 'react-router-dom';
// import { useMsal } from '@azure/msal-react';

// const ProtectedRoute = () => {
//     const [isAuthenticated, setIsAuthenticated] = useState(null);
//     const { instance } = useMsal();

//     useEffect(() => {
//         try {
//             const accounts = instance.getAllAccounts();
//             console.log('Accounts:', accounts);
//             setIsAuthenticated(accounts.length > 0);
//         } catch (error) {
//             console.error('Error fetching accounts:', error);
//             setIsAuthenticated(false);
//         }
//     }, [instance]);

//     // Loading state
//     if (isAuthenticated === null) {
//         return (
//             <div className="flex justify-center items-center h-screen">
//                 <span className="loader">Loading...</span>
//             </div>
//         );
//     }

//     const accounts = instance.getAllAccounts();
//     const userEmail = accounts[0]?.username;

//     const allowedEmails = [
//         'user1@sathvikreddy8685gmail.onmicrosoft.com', // Manager
//         'user2@sathvikreddy8685gmail.onmicrosoft.com', // Worker1
//         'user3@sathvikreddy8685gmail.onmicrosoft.com', // Worker2
//         'user4@sathvikreddy8685gmail.onmicrosoft.com', // Worker3
//         'user5@sathvikreddy8685gmail.onmicrosoft.com', // Worker4
//     ];

//     // Check authentication and allowed email
//     return isAuthenticated && allowedEmails.includes(userEmail) ? <Outlet /> : <Navigate to="/login" />;
// };

// export default ProtectedRoute;
import React, { useEffect, useState } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useMsal } from '@azure/msal-react';

const ProtectedRoute = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(null);
    const { instance } = useMsal();

    useEffect(() => {
        const accounts = instance.getAllAccounts();
        if (accounts.length > 0) {
            setIsAuthenticated(true);
        } else {
            setIsAuthenticated(false);
        }
    }, [instance]);

    // Loading state
    if (isAuthenticated === null) {
        return (
            <div className="flex justify-center items-center h-screen">
                <span className="loader">Loading...</span>
            </div>
        );
    }

    const accounts = instance.getAllAccounts();
    const userEmail = accounts[0]?.username;

    const allowedEmails = [
        'user1@sathvikreddy8685gmail.onmicrosoft.com', // Manager
        'user2@sathvikreddy8685gmail.onmicrosoft.com', // Worker1
        'user3@sathvikreddy8685gmail.onmicrosoft.com', // Worker2
        'user4@sathvikreddy8685gmail.onmicrosoft.com', // Worker3
        'user5@sathvikreddy8685gmail.onmicrosoft.com', // Worker4
    ];

    // Check authentication and allowed email
    if (isAuthenticated && allowedEmails.includes(userEmail)) {
        return <Outlet />;
    }

    return <Navigate to="/login" />;
};

export default ProtectedRoute;

// import React, { useEffect, useState } from 'react';
// import { Navigate, Outlet } from 'react-router-dom';
// import { useMsal } from '@azure/msal-react';

// const ProtectedRoute = () => {
//     const [isAuthenticated, setIsAuthenticated] = useState(null);
//     const { instance } = useMsal();

//     useEffect(() => {
//         const accounts = instance.getAllAccounts();
//         if (accounts.length > 0) {
//             setIsAuthenticated(true);
//         } else {
//             setIsAuthenticated(false);
//         }
//     }, [instance]);

//     // Loading state
//     if (isAuthenticated === null) {
//         return (
//             <div className="flex justify-center items-center h-screen">
//                 <span className="loader">Loading...</span>
//             </div>
//         );
//     }

//     const accounts = instance.getAllAccounts();
//     const userEmail = accounts[0]?.username;

//     // Fetch user roles from the token
//     const getUserRoles = (account) => {
//         const roles = account.idTokenClaims?.roles || [];
//         return roles;
//     };

//     const userRoles = getUserRoles(accounts[0]);
    
//     // Define allowed roles
//     const allowedRoles = ['Manager', 'Worker1', 'Worker2', 'Worker3', 'Worker4'];

//     // Check authentication and allowed role
//     if (isAuthenticated && userRoles.some(role => allowedRoles.includes(role))) {
//         return <Outlet />;
//     }

//     return <Navigate to="/login" />;
// };

// export default ProtectedRoute;
