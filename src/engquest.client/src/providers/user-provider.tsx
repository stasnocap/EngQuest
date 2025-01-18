import React, {createContext, ReactNode, useContext, useEffect, useState} from 'react';

// Define the shape of our user data
interface User {
  firstName: string;
  lastName: string;
  email: string;
}

export interface Level {
  value: number;
  experience: number;
  levelRequiredXp: number;
  nextLevelRequiredXp: number;
}

// Define the shape of our context
interface UserContextType {
  user: User | null;
  fetchUser: () => Promise<void>;
  level: Level;
  updateLevel: (level: Level) => void;
  login: () => void;
  logout: () => void;
}

// Create the UserContext
const UserContext = createContext<UserContextType | undefined>(undefined);

export const experienceToAchiveFirstLevel = 228;
export const experiencePerFirstQuest = 12;

function getOrCreateUnauthorizedLevel(): Level {
  const levelString = localStorage.getItem("level");
  
  if (levelString) {
    return JSON.parse(levelString);
  }
  
  const level = {value: 1, experience: 0, levelRequiredXp: 0, nextLevelRequiredXp: experienceToAchiveFirstLevel};
  
  localStorage.setItem("level", JSON.stringify(level));
  
  return level;
}

function resetLevel() {
  localStorage.removeItem("level");
}

function logout() {
  resetLevel();
  window.location.href = "/api/v1/users/logout";
}

function login() {
  window.location.href = "/api/v1/users/login";
}

// Create a provider component
export const UserProvider: React.FC<{ children: ReactNode }> = ({children}) => {
  const [user, setUser] = useState<User | null>(null);
  const [level, setLevel] = useState<Level>(getOrCreateUnauthorizedLevel());

  const fetchUser = async () => {
    const response = await fetch('/api/v1/users/me');

    if (response.status !== 200) {
      return;
    }

    const json = await response.json();

    const data = json as User;
    const level = json.level as Level;

    setUser(data);
    setLevel(level);
  };
  
  const updateLevel = (level: Level) => {
    if (!user) {
      localStorage.setItem("level", JSON.stringify(level));
    }
    
    setLevel(level);
  }

  // Fetch user on mount
  useEffect(() => {
    fetchUser();
  }, []);

  return (
    <UserContext.Provider value={{user, fetchUser, level, updateLevel, login, logout }}>
      {children}
    </UserContext.Provider>
  );
};

// Custom hook to use the UserContext
export const useUser = () => {
  const context = useContext(UserContext);
  if (!context) throw new Error('useUser must be used within a UserProvider');
  return context;
};
