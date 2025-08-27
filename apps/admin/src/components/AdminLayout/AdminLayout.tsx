import { useEffect } from "react";
import { Outlet, useMatches } from "react-router";
import { Avatar, Flex, Grid, Heading, IconButton } from "@radix-ui/themes";
import { BellIcon, MessageSquareQuoteIcon, SettingsIcon } from "lucide-react";
import { Sidebar, useSidebar } from "../Sidebar";
import { LanguageSwitch } from "../LanguageSwitch";
import { SecureRoute } from "../SecureRoute";

import classNames from "./AdminLayout.module.css";

export const AdminLayout = () => {
  const matches = useMatches();
  const { title } = (matches[matches.length - 1]?.handle as any) || {};

  const { items: adminSidebarItems } = useSidebar("/");

  useEffect(() => {
    if (title) {
      document.title = title;
    }
  }, [title]);

  return (
    <SecureRoute>
      <Grid className={classNames.root} columns="250px 1fr">
        <Flex className={classNames.nav} direction="column" gap="3">
          <Flex className={classNames.logo} justify="center">
            <Heading>Techfood</Heading>
          </Flex>
          <Sidebar>
            {adminSidebarItems.map((item, i) => (
              <Sidebar.Item key={i} {...item} />
            ))}
          </Sidebar>
        </Flex>
        <Flex className={classNames.main} direction="column">
          <Flex className={classNames.header} justify="between" align="center">
            <Heading>{title}</Heading>
            <Flex className={classNames.headerControls} align="center" gap="6">
              <Flex gap="4" align="center">
                <IconButton variant="ghost" color="gray">
                  <MessageSquareQuoteIcon />
                </IconButton>
                <IconButton variant="ghost" color="gray">
                  <BellIcon />
                </IconButton>
                <IconButton variant="ghost" color="gray">
                  <SettingsIcon />
                </IconButton>
                <Flex className={classNames.languageSwitch}>
                  <LanguageSwitch />
                </Flex>
              </Flex>
              <Avatar
                src="https://i.pravatar.cc/300"
                alt="User Avatar"
                size="3"
                fallback=""
              />
            </Flex>
          </Flex>
          <Flex className={classNames.content} direction="column">
            <Outlet />
          </Flex>
        </Flex>
      </Grid>
    </SecureRoute>
  );
};
