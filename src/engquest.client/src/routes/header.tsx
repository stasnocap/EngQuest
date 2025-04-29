import { Navbar, NavbarBrand, NavbarContent, NavbarItem, Link, Button, DropdownItem, DropdownMenu, Dropdown, DropdownTrigger, NavbarMenuToggle, NavbarMenu, NavbarMenuItem, Badge, User, Avatar } from "@heroui/react";
import { NavLink as RouterLink } from "react-router-dom";
import { useState } from "react";
import EngQuestLogo from "../icons/engquest-logo.tsx";
import Palette from "../components/palette.tsx";
import BrushIcon from "../icons/brush-icon.tsx";
import { useUser } from "../providers/user-provider.tsx";
import { useTheme } from "../providers/theme-provider.tsx";

export default function Header() {
  const { user, level, login, logout, account } = useUser();
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const { switchTheme } = useTheme();

  return (
    <Navbar shouldHideOnScroll onMenuOpenChange={setIsMenuOpen} disableAnimation className="sticky">
      <NavbarContent>
        <NavbarMenuToggle
          as="div"
          aria-label={isMenuOpen ? "Close menu" : "Open menu"}
          className="sm:hidden text-foreground cursor-pointer"
        />
        <RouterLink to="/">
          <NavbarBrand>
            <EngQuestLogo height={32} width={32} />
            <p className="text-inherit ms-2 font-bold"><span className="text-primary">ENG</span><span className="text-primary-100">QUEST</span></p>
          </NavbarBrand>
        </RouterLink>
      </NavbarContent>
      <NavbarContent className="hidden sm:flex gap-4" justify="center">
        <NavbarItem>
          <Link as="div" color="primary">
            <RouterLink to="/quests">
              Приключение
            </RouterLink>
          </Link>
        </NavbarItem>
        <NavbarItem>
          <Link as="div" color="primary">
            <RouterLink to="/leaderboard">
              Доска лидеров
            </RouterLink>
          </Link>
        </NavbarItem>
      </NavbarContent>
      <NavbarContent justify="end" className="gap-2">
        <Dropdown className="bg-background">
          <NavbarItem>
            <DropdownTrigger>
              <Button
                as="div"
                disableRipple
                className="p-0 bg-transparent data-[hover=true]:bg-transparent"
                radius="sm"
                variant="light"
                isIconOnly
              >
                <BrushIcon height={32} width={32} />
              </Button>
            </DropdownTrigger>
          </NavbarItem>
          <DropdownMenu
            aria-label="themes"
            className="w-[190px]"
          >
            <DropdownItem
              key="purple"
              onPress={() => switchTheme("purple")}
              textValue="purple"
            >
              <Palette theme="Фиолет" colors={["#F4EEFF", "#DCD6F7", "#A6B1E1", "#424874"]} isLightTheme={true} />
            </DropdownItem>
            <DropdownItem
              key="cream"
              onPress={() => switchTheme("cream")}
              textValue="cream"
            >
              <Palette theme="Крем" colors={["#FFF5E4", "#FFE3E1", "#FFD1D1", "#FF9494"]} isLightTheme={true} />
            </DropdownItem>
            <DropdownItem
              key="skin"
              onPress={() => switchTheme("skin")}
              textValue="skin"
            >
              <Palette theme="Кожа" colors={["#FFC7C7", "#FFE2E2", "#F6F6F6", "#8785A2"]} isLightTheme={true} />
            </DropdownItem>
            <DropdownItem
              key="teal"
              onPress={() => switchTheme("teal")}
              textValue="teal"
            >
              <Palette theme="Бирюза" colors={["#222831", "#393E46", "#00ADB5", "#EEEEEE"]} isLightTheme={false} />
            </DropdownItem>
            <DropdownItem
              key="navy"
              onPress={() => switchTheme("navy")}
              textValue="navy"
            >
              <Palette theme="Флот" colors={["#1B262C", "#0F4C75", "#3282B8", "#BBE1FA"]} isLightTheme={false} />
            </DropdownItem>
            <DropdownItem
              key="night"
              onPress={() => switchTheme("night")}
              textValue="night"
            >
              <Palette theme="Ночь" colors={["#27374D", "#526D82", "#9DB2BF", "#DDE6ED"]} isLightTheme={false} />
            </DropdownItem>
          </DropdownMenu>
        </Dropdown>
        {user ?
          (<>
            <Dropdown placement="bottom-end" className="bg-background">
              <DropdownTrigger>
                <div className="cursor-pointer">
                  <Badge content={`${level.value}`} color="primary" className="text-primary-50" placement="bottom-left">
                    <Avatar
                      className="transition-transform cursor-pointert block sm:hidden"
                      color="primary"
                      name="Hero"
                      size="sm"
                      src="/avatar.svg"
                    />
                    <User
                      avatarProps={{
                        src: "/avatar.svg",
                      }}
                      description={user.email}
                      name={`${user.firstName} ${user.lastName}`}
                      className="hidden sm:flex"
                    />
                  </Badge>
                </div>
              </DropdownTrigger>
              <DropdownMenu aria-label="Profile Actions" variant="flat" className="text-foreground">
                <DropdownItem key="edit-profile" description="Перезайди чтобы применить изменения" onPress={() => account()}>
                  Профиль
                </DropdownItem>
                <DropdownItem key="logout" color="danger" onPress={() => logout()}>
                  Выйти
                </DropdownItem>
              </DropdownMenu>
            </Dropdown>
          </>)
          :
          (<div className="gap-1 hidden md:flex">

            <NavbarItem>
              <Button as={Link} color="primary" variant="light" onPress={() => login()}>
                Войти
              </Button>
            </NavbarItem>
          </div>)}
      </NavbarContent>
      <NavbarMenu>
        <NavbarMenuItem key="quests">
          <Link
            as="div"
            color="primary"
            className="w-full"
            size="lg"
          >
            <RouterLink to="/quests">
              Приключение
            </RouterLink>
          </Link>
        </NavbarMenuItem>
        <NavbarMenuItem key="leaderboard">
          <Link
            as="div"
            color="primary"
            className="w-full"
            size="lg"
          >
            <RouterLink to="/leaderboard">
              Доска лидеров
            </RouterLink>
          </Link>
        </NavbarMenuItem>
        {user ? (
          <>
            <NavbarMenuItem key="account">
              <Link
                color="primary"
                className="w-full"
                onPress={() => account()}
                size="lg"
              >
                Профиль
              </Link>
            </NavbarMenuItem>
            <NavbarMenuItem key="logout">
              <Link
                color="danger"
                className="w-full"
                onPress={() => logout()}
                size="lg"
              >
                Выйти
              </Link>
            </NavbarMenuItem>
          </>
        ) : (
          <>
            <NavbarMenuItem key="login">
              <Link
                className="w-full text-primary"
                onPress={() => login()}
              >
                Войти
              </Link>
            </NavbarMenuItem>
          </>
        )}
      </NavbarMenu>
    </Navbar>
  );
}
