param(
  [string]$BaseUrl = 'http://localhost:5004',
  [string]$UserName = 'Programmer',
  [string]$Password = 'prog@123'
)

$ErrorActionPreference = 'Stop'

chcp 65001 | Out-Null
[Console]::InputEncoding = [System.Text.UTF8Encoding]::new()
[Console]::OutputEncoding = [System.Text.UTF8Encoding]::new()

function Invoke-CurlJsonGet {
  param(
    [Parameter(Mandatory = $true)][string]$Url,
    [Parameter(Mandatory = $false)][string]$Jwt
  )

  $outFile = New-TemporaryFile
  try {
    $args = @('-sS', '-k', $Url, '--connect-timeout', '20', '--max-time', '120', '-o', $outFile.FullName)
    if ($Jwt) {
      $args += @('-H', "Authorization: Bearer $Jwt")
    }

    & curl.exe @args | Out-Null
    if ($LASTEXITCODE -ne 0) {
      throw "GET request failed: $Url"
    }

    $raw = Get-Content -LiteralPath $outFile.FullName -Raw -Encoding UTF8
    if ([string]::IsNullOrWhiteSpace($raw)) {
      throw "Empty response from: $Url"
    }

    return $raw | ConvertFrom-Json
  }
  finally {
    if (Test-Path $outFile.FullName) {
      Remove-Item -LiteralPath $outFile.FullName -Force
    }
  }
}

function Invoke-CurlJsonPost {
  param(
    [Parameter(Mandatory = $true)][string]$Url,
    [Parameter(Mandatory = $true)]$BodyObject,
    [Parameter(Mandatory = $false)][string]$Jwt
  )

  $bodyFile = New-TemporaryFile
  $outFile = New-TemporaryFile
  try {
    $json = $BodyObject | ConvertTo-Json -Depth 20 -Compress
    [System.IO.File]::WriteAllText($bodyFile.FullName, $json, [System.Text.UTF8Encoding]::new($false))

    $args = @('-sS', '-k', '-X', 'POST', $Url, '-H', 'Content-Type: application/json; charset=utf-8', '--data-binary', "@$($bodyFile.FullName)", '--connect-timeout', '20', '--max-time', '120', '-o', $outFile.FullName)
    if ($Jwt) {
      $args += @('-H', "Authorization: Bearer $Jwt")
    }

    & curl.exe @args | Out-Null
    if ($LASTEXITCODE -ne 0) {
      throw "POST request failed: $Url"
    }

    $raw = Get-Content -LiteralPath $outFile.FullName -Raw -Encoding UTF8
    if ([string]::IsNullOrWhiteSpace($raw)) {
      throw "Empty response from: $Url"
    }

    return $raw | ConvertFrom-Json
  }
  finally {
    if (Test-Path $bodyFile.FullName) {
      Remove-Item -LiteralPath $bodyFile.FullName -Force
    }
    if (Test-Path $outFile.FullName) {
      Remove-Item -LiteralPath $outFile.FullName -Force
    }
  }
}

function Ensure-Success {
  param(
    [Parameter(Mandatory = $true)]$Response,
    [Parameter(Mandatory = $true)][string]$Message
  )

  if (-not $Response.isSuccessful) {
    throw "$Message: $($Response.errorMessages -join '; ')"
  }
}

$loginResponse = Invoke-CurlJsonPost "$BaseUrl/Auth/LoginAsync" @{ userName = $UserName; password = $Password }
Ensure-Success $loginResponse 'Login failed'
$jwt = $loginResponse.data.token
$userId = $loginResponse.data.user.id

$storesResponse = Invoke-CurlJsonGet "$BaseUrl/Store/GetByOwnerAsync" $jwt
Ensure-Success $storesResponse 'Cannot fetch stores'
$stores = @()
if ($storesResponse.data) { $stores = @($storesResponse.data) }

$primaryStore = $stores | Where-Object { $_.storeEnglishName -eq 'factormaker-demo' } | Select-Object -First 1
if (-not $primaryStore) {
  $createStoreResponse = Invoke-CurlJsonPost "$BaseUrl/Store/InsertAsync" @{
    storeEnglishName = 'factormaker-demo'
    name = 'فروشگاه نمونه فکتور میکر'
    url = 'https://factormaker.local/demo'
    description = 'فروشگاه نمایشی برای تست سناریوهای فروش و داشبورد'
    tagline = 'ویترین نمونه برای فاکتور، کالا و محتوای فروشگاه'
    themePalette = 'forest'
    isPublic = $true
    socialLinks = @(
      @{
        platform = 'website'
        url = 'https://factormaker.local/demo'
        label = 'مشاهده و سفارش'
        isPrimary = $true
      }
    )
  } $jwt
  Ensure-Success $createStoreResponse 'Cannot create store'
  $primaryStore = $createStoreResponse.data
}

if (-not $primaryStore.tagline -or -not $primaryStore.isPublic -or -not $primaryStore.socialLinks -or @($primaryStore.socialLinks).Count -eq 0) {
  $links = @()
  if ($primaryStore.socialLinks) { $links = @($primaryStore.socialLinks) }
  if ($links.Count -eq 0) {
    $links = @(
      @{
        platform = 'website'
        url = $(if ($primaryStore.url) { $primaryStore.url } else { 'https://factormaker.local/demo' })
        label = 'مشاهده و سفارش'
        isPrimary = $true
      }
    )
  }

  $updateStoreResponse = Invoke-CurlJsonPost "$BaseUrl/Store/UpdateAsync" @{
    id = $primaryStore.id
    ownerId = $primaryStore.ownerId
    storeEnglishName = $primaryStore.storeEnglishName
    name = $primaryStore.name
    url = $(if ($primaryStore.url) { $primaryStore.url } else { 'https://factormaker.local/demo' })
    description = $primaryStore.description
    tagline = $(if ($primaryStore.tagline) { $primaryStore.tagline } else { 'ویترین نمونه برای فاکتور، کالا و محتوای فروشگاه' })
    themePalette = $(if ($primaryStore.themePalette) { $primaryStore.themePalette } else { 'forest' })
    isPublic = $true
    logoId = $primaryStore.logoId
    coverImageId = $primaryStore.coverImageId
    socialLinks = $links
  } $jwt
  Ensure-Success $updateStoreResponse 'Cannot update demo store profile'
  $primaryStore = $updateStoreResponse.data
}

$storeId = $primaryStore.id

$categoryNames = @(
  'قهوه و نوشیدنی گرم', 'نوشیدنی سرد', 'صبحانه', 'اسنک', 'لبنیات', 'پروتئینی',
  'میوه و سبزیجات', 'غلات و حبوبات', 'ادویه و چاشنی', 'بهداشتی', 'شوینده', 'لوازم تحریر'
)

$productSeeds = @(
  @{ name = 'قهوه ترک'; price = 95000; category = 'قهوه و نوشیدنی گرم' },
  @{ name = 'اسپرسو سینگل'; price = 89000; category = 'قهوه و نوشیدنی گرم' },
  @{ name = 'لاته'; price = 118000; category = 'قهوه و نوشیدنی گرم' },
  @{ name = 'آب معدنی'; price = 14000; category = 'نوشیدنی سرد' },
  @{ name = 'آب پرتقال طبیعی'; price = 52000; category = 'نوشیدنی سرد' },
  @{ name = 'لیموناد'; price = 46000; category = 'نوشیدنی سرد' },
  @{ name = 'نان تست'; price = 32000; category = 'صبحانه' },
  @{ name = 'عسل طبیعی'; price = 168000; category = 'صبحانه' },
  @{ name = 'کره بادام زمینی'; price = 112000; category = 'صبحانه' },
  @{ name = 'چیپس خلالی'; price = 43000; category = 'اسنک' },
  @{ name = 'بیسکویت کره ای'; price = 38000; category = 'اسنک' },
  @{ name = 'شکلات تلخ'; price = 76000; category = 'اسنک' },
  @{ name = 'شیر کم چرب'; price = 29000; category = 'لبنیات' },
  @{ name = 'ماست موسیر'; price = 54000; category = 'لبنیات' },
  @{ name = 'پنیر سفید'; price = 62000; category = 'لبنیات' },
  @{ name = 'فیله مرغ'; price = 185000; category = 'پروتئینی' },
  @{ name = 'گوشت چرخ کرده'; price = 268000; category = 'پروتئینی' },
  @{ name = 'تخم مرغ 15 عددی'; price = 94000; category = 'پروتئینی' },
  @{ name = 'گوجه فرنگی'; price = 34000; category = 'میوه و سبزیجات' },
  @{ name = 'خیار گلخانه ای'; price = 28000; category = 'میوه و سبزیجات' },
  @{ name = 'سیب قرمز'; price = 56000; category = 'میوه و سبزیجات' },
  @{ name = 'برنج ایرانی'; price = 298000; category = 'غلات و حبوبات' },
  @{ name = 'عدس'; price = 64000; category = 'غلات و حبوبات' },
  @{ name = 'ماکارونی'; price = 33000; category = 'غلات و حبوبات' },
  @{ name = 'رب گوجه'; price = 62000; category = 'ادویه و چاشنی' },
  @{ name = 'فلفل سیاه'; price = 41000; category = 'ادویه و چاشنی' },
  @{ name = 'زعفران یک گرمی'; price = 195000; category = 'ادویه و چاشنی' },
  @{ name = 'مایع دستشویی'; price = 72000; category = 'بهداشتی' },
  @{ name = 'خمیر دندان'; price = 48000; category = 'بهداشتی' },
  @{ name = 'مسواک نرم'; price = 34000; category = 'بهداشتی' },
  @{ name = 'مایع ظرفشویی'; price = 69000; category = 'شوینده' },
  @{ name = 'پودر لباسشویی'; price = 138000; category = 'شوینده' },
  @{ name = 'شیشه پاک کن'; price = 52000; category = 'شوینده' },
  @{ name = 'دفتر 80 برگ'; price = 36000; category = 'لوازم تحریر' },
  @{ name = 'خودکار آبی'; price = 15000; category = 'لوازم تحریر' },
  @{ name = 'مداد مشکی'; price = 12000; category = 'لوازم تحریر' }
)

$customerSeeds = @(
  @{ firstName = 'مهدی'; lastName = 'رحیمی'; nationalCode = '1401000001'; gender = 0 },
  @{ firstName = 'سارا'; lastName = 'قاسمی'; nationalCode = '1401000002'; gender = 1 },
  @{ firstName = 'الهام'; lastName = 'کریمی'; nationalCode = '1401000003'; gender = 1 },
  @{ firstName = 'رضا'; lastName = 'جعفری'; nationalCode = '1401000004'; gender = 0 },
  @{ firstName = 'ندا'; lastName = 'احمدی'; nationalCode = '1401000005'; gender = 1 },
  @{ firstName = 'علی'; lastName = 'صبوری'; nationalCode = '1401000006'; gender = 0 },
  @{ firstName = 'حسین'; lastName = 'موسوی'; nationalCode = '1401000007'; gender = 0 },
  @{ firstName = 'بهاره'; lastName = 'خسروی'; nationalCode = '1401000008'; gender = 1 },
  @{ firstName = 'نگار'; lastName = 'حسینی'; nationalCode = '1401000009'; gender = 1 },
  @{ firstName = 'کیان'; lastName = 'عباسی'; nationalCode = '1401000010'; gender = 0 },
  @{ firstName = 'پریسا'; lastName = 'زارع'; nationalCode = '1401000011'; gender = 1 },
  @{ firstName = 'نوید'; lastName = 'یوسفی'; nationalCode = '1401000012'; gender = 0 }
)

$categoriesResponse = Invoke-CurlJsonGet "$BaseUrl/Category/GetByStoreIdAsync?storeId=$storeId" $jwt
Ensure-Success $categoriesResponse 'Cannot fetch categories'
$categories = @()
if ($categoriesResponse.data) { $categories = @($categoriesResponse.data) }

foreach ($categoryName in $categoryNames) {
  $existing = $categories | Where-Object { $_.name -eq $categoryName } | Select-Object -First 1
  if ($existing) { continue }

  $response = Invoke-CurlJsonPost "$BaseUrl/Category/InsertAsync" @{ storeId = $storeId; name = $categoryName } $jwt
  Ensure-Success $response "Cannot create category $categoryName"
  $categories += $response.data
}

$productsResponse = Invoke-CurlJsonGet "$BaseUrl/Product/GetByStoreIdAsync?storeId=$storeId" $jwt
Ensure-Success $productsResponse 'Cannot fetch products'
$products = @()
if ($productsResponse.data) { $products = @($productsResponse.data) }

$sortOrder = 1
foreach ($seed in $productSeeds) {
  $existing = $products | Where-Object { $_.name -eq $seed.name } | Select-Object -First 1
  if ($existing) { $sortOrder++; continue }

  $category = $categories | Where-Object { $_.name -eq $seed.category } | Select-Object -First 1
  $response = Invoke-CurlJsonPost "$BaseUrl/Product/InsertAsync" @{
    storeId = $storeId
    name = $seed.name
    price = [int]$seed.price
    categoryId = $category.id
    shortDescription = "$($seed.name) از محصولات فروشگاه نمونه"
    showOnLanding = $true
    sortOrder = $sortOrder
  } $jwt
  Ensure-Success $response "Cannot create product $($seed.name)"
  $products += $response.data
  $sortOrder++
}

$customersResponse = Invoke-CurlJsonGet "$BaseUrl/Customer/GetByStoreIdAsync?storeId=$storeId" $jwt
Ensure-Success $customersResponse 'Cannot fetch customers'
$customers = @()
if ($customersResponse.data) { $customers = @($customersResponse.data) }

foreach ($seed in $customerSeeds) {
  $existing = $customers | Where-Object { $_.nationalCode -eq $seed.nationalCode } | Select-Object -First 1
  if ($existing) { continue }

  $response = Invoke-CurlJsonPost "$BaseUrl/Customer/InsertAsync" @{
    firstName = $seed.firstName
    lastName = $seed.lastName
    nationalCode = $seed.nationalCode
    gender = $seed.gender
    storeId = $storeId
  } $jwt
  Ensure-Success $response "Cannot create customer $($seed.firstName) $($seed.lastName)"
  $customers += $response.data
}

$factorsResponse = Invoke-CurlJsonGet "$BaseUrl/Factor/GetByStoreIdAsync?storeId=$storeId" $jwt
Ensure-Success $factorsResponse 'Cannot fetch factors'
$factors = @()
if ($factorsResponse.data) { $factors = @($factorsResponse.data) }

$targetClosedFactors = 36
$existingClosedFactors = @($factors | Where-Object { $_.isClosed -eq $true }).Count
$newFactorsCount = [Math]::Max(0, $targetClosedFactors - $existingClosedFactors)

for ($i = 0; $i -lt $newFactorsCount; $i++) {
  $customer = Get-Random -InputObject $customers
  $daysAgo = Get-Random -Minimum 0 -Maximum 45
  $hour = Get-Random -Minimum 8 -Maximum 22
  $minute = Get-Random -Minimum 0 -Maximum 59
  $sellDate = (Get-Date).Date.AddDays(-$daysAgo).AddHours($hour).AddMinutes($minute)

  $factorResponse = Invoke-CurlJsonPost "$BaseUrl/Factor/InsertAsync" @{
    ownerId = $customer.id
    storeId = $storeId
    description = "فاکتور فروش روزانه شماره $($existingClosedFactors + $i + 1)"
    sellDateTime = $sellDate.ToString('o')
    isClosed = $false
    totalPrice = 0
  } $jwt
  Ensure-Success $factorResponse 'Cannot create factor'
  $factor = $factorResponse.data

  $itemsCount = Get-Random -Minimum 2 -Maximum 6
  for ($j = 0; $j -lt $itemsCount; $j++) {
    $product = Get-Random -InputObject $products
    $quantity = Get-Random -Minimum 1 -Maximum 5
    $offPercent = (Get-Random -InputObject @(0, 0, 0, 5, 10, 15))

    $itemResponse = Invoke-CurlJsonPost "$BaseUrl/FactorItem/InsertAsync" @{
      factorId = $factor.id
      productId = $product.id
      quantity = $quantity
      offPercent = $offPercent
      price = [int]$product.price
      description = "آیتم فروش $($product.name)"
    } $jwt
    Ensure-Success $itemResponse 'Cannot create factor item'
  }

  $factorDetailResponse = Invoke-CurlJsonGet "$BaseUrl/Factor/GetFactorWithItemsByIdAsync?id=$($factor.id)" $jwt
  Ensure-Success $factorDetailResponse 'Cannot fetch factor detail'
  $factorDetail = $factorDetailResponse.data
  $factorDetail.isClosed = $true

  $closeResponse = Invoke-CurlJsonPost "$BaseUrl/Factor/UpdateAsync" $factorDetail $jwt
  Ensure-Success $closeResponse 'Cannot close factor'
}

$contentSeeds = @(
  @{
    title = 'پیشنهاد ویژه صبحانه'
    body = 'ترکیب تازه صبحانه با تخفیف ویژه برای مشتریان فروشگاه نمونه.'
    extendedBody = 'در این هفته می‌توانید از مجموعه صبحانه شامل نان تست، عسل و شیر با قیمت مناسب استفاده کنید.'
    isHot = $true
    isPublished = $true
  },
  @{
    title = 'نوشیدنی‌های تابستانی'
    body = 'لیموناد و آب پرتقال طبیعی برای روزهای گرم.'
    extendedBody = 'نوشیدنی‌های سرد فروشگاه با تازگی روزانه آماده سرو می‌شوند.'
    isHot = $true
    isPublished = $true
  },
  @{
    title = 'راهنمای خرید هفتگی'
    body = 'پیشنهاد خرید هفتگی برای خانواده‌های پرمصرف.'
    extendedBody = 'در این مطلب ترکیبی از محصولات پرتکرار فروشگاه را برای خرید هفتگی معرفی کرده‌ایم.'
    isHot = $false
    isPublished = $true
  }
)

foreach ($seed in $contentSeeds) {
  $existingPosts = Invoke-CurlJsonGet "$BaseUrl/BlogPost/GetByOwnerIdAsync?ownerId=$userId" $jwt
  Ensure-Success $existingPosts 'Cannot fetch blog posts'
  $existing = @()
  if ($existingPosts.data) { $existing = @($existingPosts.data) }
  $found = $existing | Where-Object { $_.title -eq $seed.title } | Select-Object -First 1
  if ($found) { continue }

  $postResponse = Invoke-CurlJsonPost "$BaseUrl/BlogPost/InsertAsync" @{
    ownerId = $userId
    title = $seed.title
    body = $seed.body
    extendedBody = $seed.extendedBody
    isHot = [bool]$seed.isHot
    isPublished = [bool]$seed.isPublished
    isStarRatingEnabled = $false
    publishDateTime = (Get-Date).ToString('o')
    visitCount = 0
    starRate = 0
    postCategories = @()
    images = @()
  } $jwt
  Ensure-Success $postResponse "Cannot create blog post $($seed.title)"
}

$verifyProductsResponse = Invoke-CurlJsonGet "$BaseUrl/Product/GetByStoreIdAsync?storeId=$storeId" $jwt
$verifyCustomersResponse = Invoke-CurlJsonGet "$BaseUrl/Customer/GetByStoreIdAsync?storeId=$storeId" $jwt
$verifyFactorsResponse = Invoke-CurlJsonGet "$BaseUrl/Factor/GetByStoreIdAsync?storeId=$storeId" $jwt
Ensure-Success $verifyProductsResponse 'Product verification failed'
Ensure-Success $verifyCustomersResponse 'Customer verification failed'
Ensure-Success $verifyFactorsResponse 'Factor verification failed'

[ordered]@{
  baseUrl = $BaseUrl
  storeId = $storeId
  storeName = $primaryStore.name
  productsCount = @($verifyProductsResponse.data).Count
  customersCount = @($verifyCustomersResponse.data).Count
  factorsCount = @($verifyFactorsResponse.data).Count
  closedFactorsCount = @($verifyFactorsResponse.data | Where-Object { $_.isClosed -eq $true }).Count
  blogPostsCount = @((Invoke-CurlJsonGet "$BaseUrl/BlogPost/GetByOwnerIdAsync?ownerId=$userId" $jwt).data).Count
  sampleLogin = @{ userName = $UserName; password = $Password }
} | ConvertTo-Json -Depth 6
