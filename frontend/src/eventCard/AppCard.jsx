// AppCard.jsx
import React, { useState } from "react";
import Sidebar from "../components/Sidebar.jsx";
import EventCard from "./EventCard.jsx";

const App = () => {
  const [isOpen, setIsOpen] = useState(false);
  return (
    <>
      <Sidebar isOpen={isOpen} toggleSidebar={() => setIsOpen(o => !o)} />
      <EventCard />
    </>
  );
};
export default App;