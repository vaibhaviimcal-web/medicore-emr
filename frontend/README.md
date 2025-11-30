# MediCore EMR - Frontend

React PWA optimized for iPad use.

## Setup Instructions

### Prerequisites
- Node.js 18+
- npm or yarn

### Installation

1. **Install dependencies:**
```bash
npm install
```

2. **Create environment file:**
```bash
cp .env.example .env
```

Edit `.env` and update the API URL if needed:
```
VITE_API_URL=http://localhost:5000/api/v1
```

3. **Run development server:**
```bash
npm run dev
```

Frontend will be available at: `http://localhost:3000`

### Building for Production

```bash
npm run build
```

The build output will be in the `dist/` folder.

## Testing on iPad Simulator

### Chrome DevTools Method:
1. Open the app in Chrome
2. Press `F12` to open DevTools
3. Click Toggle Device Toolbar (`Ctrl+Shift+M`)
4. Select "iPad Pro" from the device dropdown
5. Click the rotate icon to switch to landscape mode
6. Test all features!

### Supported iPad Sizes:
- iPad Mini (744 x 1133 px)
- iPad Air (820 x 1180 px)
- iPad Pro 11" (834 x 1194 px)
- iPad Pro 12.9" (1024 x 1366 px)

## Features

- **Patient Management** - Registration, search, history
- **OPD Case Sheet** - Quick data entry with pre-loaded options
- **E-Prescribing** - One-click medicine selection
- **Billing** - Integrated payment tracking
- **PWA Support** - Install as app on iPad
- **Offline Mode** - Works without internet
- **Touch Optimized** - Large buttons, easy navigation

## Project Structure

```
src/
├── components/       # React components
│   ├── Dashboard.jsx
│   ├── PatientRegistration.jsx
│   ├── CaseSheet.jsx
│   ├── PrescriptionBuilder.jsx
│   └── Billing.jsx
├── services/        # API service layer
│   └── api.js
├── styles/          # CSS files
│   └── ipad.css
├── App.jsx          # Main app component
└── main.jsx         # Entry point
```

## Deployment

Deploy to Vercel, Netlify, or any static hosting:

```bash
npm run build
# Upload dist/ folder to your hosting
```

## PWA Installation

On iPad:
1. Open the app in Safari
2. Tap the Share button
3. Select "Add to Home Screen"
4. The app will install like a native app!
