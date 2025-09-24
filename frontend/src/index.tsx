import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { MsalProvider } from '@azure/msal-react';
import { PublicClientApplication, type Configuration } from '@azure/msal-browser';

const clientId = (import.meta as any).env?.VITE_AZURE_AD_CLIENT_ID || (window as any)._env_?.VITE_AZURE_AD_CLIENT_ID;
const tenantId = (import.meta as any).env?.VITE_AZURE_AD_TENANT_ID || (window as any)._env_?.VITE_AZURE_AD_TENANT_ID;
const authority = `https://login.microsoftonline.com/${tenantId}`;

const msalConfig: Configuration = {
  auth: {
    clientId: clientId,
    authority,
    navigateToLoginRequestUrl: true
  },
  cache: { cacheLocation: 'localStorage' }
};

const msalInstance = new PublicClientApplication(msalConfig);

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
// No-op change to trigger SWA deploy after token rotation.
root.render(
  <React.StrictMode>
    <MsalProvider instance={msalInstance}>
      <App />
    </MsalProvider>
  </React.StrictMode>
);
