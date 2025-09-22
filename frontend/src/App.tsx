import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import axios from 'axios';
import { useMsal, useIsAuthenticated, AuthenticatedTemplate, UnauthenticatedTemplate } from '@azure/msal-react';
import { InteractionRequiredAuthError } from '@azure/msal-browser';

const apiBase = import.meta.env.VITE_API_BASE || 'http://localhost:8080';

const Home = () => {
  const isAuthenticated = useIsAuthenticated();
  const { instance, accounts } = useMsal();

  const login = async () => {
    await instance.loginPopup({ scopes: ["User.Read"] });
  };
  const logout = () => instance.logoutPopup();

  const callApi = async () => {
    try {
      const account = accounts[0];
      const token = await instance.acquireTokenSilent({
        account,
        scopes: ["api://" + import.meta.env.VITE_AZURE_AD_BACKEND_CLIENT_ID + "/.default"]
      }).catch(async (e: unknown) => {
        if (e instanceof InteractionRequiredAuthError) {
          const res = await instance.acquireTokenPopup({ scopes: ["api://" + import.meta.env.VITE_AZURE_AD_BACKEND_CLIENT_ID + "/.default"] });
          return res;
        }
        throw e;
      });

      const resp = await axios.get(`${apiBase}/health/db`, {
        headers: { Authorization: `Bearer ${token.accessToken}` }
      });
      alert(JSON.stringify(resp.data));
    } catch (err: any) {
      alert(err?.message || 'API call failed');
    }
  };

  const callSecure = async () => {
    try {
      const account = accounts[0];
      const token = await instance.acquireTokenSilent({
        account,
        scopes: ["api://" + import.meta.env.VITE_AZURE_AD_BACKEND_CLIENT_ID + "/.default"]
      });
      const resp = await axios.get(`${apiBase}/api/secure/ping`, {
        headers: { Authorization: `Bearer ${token.accessToken}` }
      });
      alert(JSON.stringify(resp.data));
    } catch (err: any) {
      alert(err?.message || 'Secure API call failed');
    }
  };

  return (
    <div>
      <h1>Dans TCG System</h1>
      <p style={{ fontSize: '0.9rem', color: '#555' }}>API base: {apiBase}</p>
      <AuthenticatedTemplate>
        <p>Signed in.</p>
        <button onClick={callApi}>Call API (/health/db)</button>
  <button onClick={callSecure}>Call Protected API (/api/secure/ping)</button>
        <button onClick={logout}>Logout</button>
      </AuthenticatedTemplate>
      <UnauthenticatedTemplate>
        <p>You are not signed in.</p>
        <button onClick={login}>Login</button>
      </UnauthenticatedTemplate>
    </div>
  );
};

const Protected: React.FC = () => <h2>Protected content</h2>;

const ProtectedRoute: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const isAuthenticated = useIsAuthenticated();
  return isAuthenticated ? <>{children}</> : <Navigate to="/" replace />;
};

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route
          path="/protected"
          element={
            <ProtectedRoute>
              <Protected />
            </ProtectedRoute>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;
