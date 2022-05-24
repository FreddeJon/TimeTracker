import { useState } from "react";

const useActiveProject = () => {
  const [activeProject, setActiveProject] = useState(null);
  return { activeProject, setActiveProject };
};

export default useActiveProject;
