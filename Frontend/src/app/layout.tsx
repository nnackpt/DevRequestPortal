import type { Metadata } from "next";
import "./globals.css";
import Providers from "@/Components/Providers";

export const metadata: Metadata = {
  title: "Dev Request Portal | Autoliv"
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Providers>
          {children}
        </Providers>
      </body>
    </html>
  );
}
