import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import './index.css'
import {createBrowserRouter, RouterProvider, useNavigate,} from "react-router-dom";
import Home from "./routes/home.tsx";
import Quests from "./routes/quests/quests.tsx";
import Objective from "./routes/quests/objective.tsx";
import Infos from "./routes/quests/infos.tsx";
import {HeroUIProvider} from "@heroui/react";
import {ThemeProvider} from "./providers/theme-provider.tsx";
import {UserProvider} from "./providers/user-provider.tsx";
import Root from "./routes/root.tsx";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App/>,
    children: [
      {
        path: "",
        element: <Home/>,
      },
      {
        path: "quests",
        element: <Quests/>
      },
      {
        path: "quests/:questId",
        element: <Objective/>,
      },
      {
        path: "quests/:questId/info",
        element: < Infos/>,
      },
    ]
  },
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router}/>
  </StrictMode>
);


function App() {
  const navigate = useNavigate();
  return (
    <HeroUIProvider className="min-h-screen" navigate={navigate}>
      <ThemeProvider>
        <UserProvider>
          <Root/>
        </UserProvider>
      </ThemeProvider>
    </HeroUIProvider>
  )
} 