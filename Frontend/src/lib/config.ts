export interface AppConfig {
    API_BASE_URL: string
}

let cached: AppConfig | null = null

export async function getConfig(): Promise<AppConfig> {
    if (cached) return cached
    const res = await fetch("/config.json")
    cached = await res.json()
    return cached!
}