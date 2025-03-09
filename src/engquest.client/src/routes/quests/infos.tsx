import {MDXProvider} from '@mdx-js/react';
import Info1 from './info/info1.mdx';
import Info2 from './info/info2.mdx';
import Info3 from './info/info3.mdx';
import Info4 from './info/info4.mdx';
import Info5 from './info/info5.mdx';
import Info6 from './info/info6.mdx';
import Info7 from './info/info7.mdx';
import Info8 from './info/info8.mdx';
import Info9 from './info/info9.mdx';
import Info10 from './info/info10.mdx';
import Info11 from './info/info11.mdx';
import Info12 from './info/info12.mdx';
import Info13 from './info/info13.mdx';
import Info14 from './info/info14.mdx';
import Info15 from './info/info15.mdx';
import Info16 from './info/info16.mdx';
import Info17 from './info/info17.mdx';
import Info18 from './info/info18.mdx';
import Info19 from './info/info19.mdx';
import Info20 from './info/info20.mdx';
import Info21 from './info/info21.mdx';
import Info22 from './info/info22.mdx';
import Info23 from './info/info23.mdx';
import Info24 from './info/info24.mdx';
import Info25 from './info/info25.mdx';
import Info26 from './info/info26.mdx';
import Info27 from './info/info27.mdx';
import Info28 from './info/info28.mdx';
import Info29 from './info/info29.mdx';
import Info30 from './info/info30.mdx';
import Info31 from './info/info31.mdx';
import {BreadcrumbItem, Breadcrumbs, Button} from "@heroui/react";
import './info/info.css';
import {useLocation} from 'react-router-dom';
import {Quest} from "./quests.ts";

export default function Infos() {
  const location = useLocation();
  const {quest}: { quest: Quest } = location.state;
  const Info = getComponentByQuestId(Number(quest.id));

  const components = {
    em(properties: any) {
      return <i {...properties} />
    }
  }

  const scrollToTop = () => {
    window.scrollTo({top: 0, behavior: 'smooth'});
  };

  return (
    <>
      <Breadcrumbs itemClasses={{item: "text-lg"}} underline="hover" className="mb-3" color="primary">
        <BreadcrumbItem href="/quests">Квеcты</BreadcrumbItem>
        <BreadcrumbItem href={`/quests/${quest.id}/info`}>Квест {quest.id}: {quest.name}</BreadcrumbItem>
      </Breadcrumbs>
      <div className="info text-lg">
        <MDXProvider components={components}>
          <Info/>
        </MDXProvider>
      </div>
      <Button onClick={scrollToTop} size="lg" variant="flat" radius="full" color="primary"> 
        Вверх 
      </Button>    
    </>
  );
}

const getComponentByQuestId = (questId: number) => {
  const components: { [key: number]: React.FC } = {
    1: Info1,
    2: Info2,
    3: Info3,
    4: Info4,
    5: Info5,
    6: Info6,
    7: Info7,
    8: Info8,
    9: Info9,
    10: Info10,
    11: Info11,
    12: Info12,
    13: Info13,
    14: Info14,
    15: Info15,
    16: Info16,
    17: Info17,
    18: Info18,
    19: Info19,
    20: Info20,
    21: Info21,
    22: Info22,
    23: Info23,
    24: Info24,
    25: Info25,
    26: Info26,
    27: Info27,
    28: Info28,
    29: Info29,
    30: Info30,
    31: Info31
  };
  return components[questId] || NotFound;
};

const NotFound: React.FC = () => <div>Урок не найден :(</div>;