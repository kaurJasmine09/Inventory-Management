import {Outlet, Navigate} from 'react-router-dom';

function ProtectedRoute({redirectPath = '/'}){
  const authenticated = localStorage.getItem('authenticated');
    if (!authenticated) {
      return <Navigate to={redirectPath} replace />;
    }
  
    return <Outlet />;
};

export default ProtectedRoute;