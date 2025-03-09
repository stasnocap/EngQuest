// src/components/MyCustomSnippet.tsx
import React, { ReactNode } from 'react';
import {Snippet} from "@heroui/snippet";

interface InfoSnippetProps {
  children: ReactNode;
}

const Highlight: React.FC<InfoSnippetProps> = ({ children }) => {
  return (
    <Snippet hideCopyButton hideSymbol className="bg-primary-50 text-primary">
      {children}
    </Snippet>
  );
};

export default Highlight;
